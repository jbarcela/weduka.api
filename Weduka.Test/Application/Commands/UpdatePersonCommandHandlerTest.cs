using Moq;
using Weduka.Application.Commands.Person.Update;
using Weduka.Application.Exceptions;
using Weduka.Application.Shared;
using Weduka.Domain.Entities;
using Weduka.Domain.Interfaces;
using Weduka.Domain.Repositories;
using Xunit;

namespace Weduka.Test.Application.Commands;

public class UpdatePersonCommandHandlerTest
{
    private readonly Mock<IPersonRepository> _mockPersonRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly UpdatePersonCommandHandler _handler;

    public UpdatePersonCommandHandlerTest()
    {
        _mockPersonRepository = new Mock<IPersonRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new UpdatePersonCommandHandler(_mockPersonRepository.Object, _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task Handle_PersonExists_UpdatePerson()
    {
        // Arrange
        var person = new Person { Id = 1, Name = "Person 1" };
        var contacts = new []
        {
            new ContactDTO
            {
                Type = "phone", 
                Value = "51999999999"
            }
        };
        var request = new UpdatePersonCommandRequest { Id = 1, Name = "Person 1 Updated", Contacts = contacts };
        _mockPersonRepository.Setup(x => x.GetByIdAsync(request.Id)).ReturnsAsync(person);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result);
        Assert.Equal(request.Name, person.Name);
        Assert.Collection(person.Contacts,
            c => Assert.Equal(contacts[0].Type, c.Type)
        );
        _mockPersonRepository.Verify(x => x.Update(person), Times.Once);
        _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Once);
    }
    
    [Fact]
    public async Task Handle_PersonNotFound_ThrowException()
    {
        // Arrange
        var contacts = new []
        {
            new ContactDTO
            {
                Type = "phone", 
                Value = "51999999999"
            }
        };
        var request = new UpdatePersonCommandRequest { Id = 1, Name = "Person 1 Updated", Contacts = contacts };
        _mockPersonRepository.Setup(x => x.GetByIdAsync(request.Id)).ReturnsAsync(null as Person);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(request, CancellationToken.None));
        _mockPersonRepository.Verify(x => x.Update(It.IsAny<Person>()), Times.Never);
        _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Never);
    }
}