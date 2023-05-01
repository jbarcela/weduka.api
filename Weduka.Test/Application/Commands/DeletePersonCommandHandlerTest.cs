using Moq;
using Weduka.Application.Commands.Person.Delete;
using Weduka.Application.Commands.Person.Update;
using Weduka.Application.Exceptions;
using Weduka.Domain.Entities;
using Weduka.Domain.Interfaces;
using Weduka.Domain.Repositories;
using Xunit;

namespace Weduka.Test.Application.Commands;

public class DeletePersonCommandHandlerTest
{
    private readonly Mock<IPersonRepository> _mockPersonRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly DeletePersonCommandHandler _handler;

    public DeletePersonCommandHandlerTest()
    {
        _mockPersonRepository = new Mock<IPersonRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new DeletePersonCommandHandler(_mockPersonRepository.Object, _mockUnitOfWork.Object);
    }
    
    [Fact]
    public async Task Handle_PersonExists_ShouldRemovePerson()
    {
        // Arrange
        var personId = 1;
        _mockPersonRepository.Setup(x => x.GetByIdAsync(personId))
            .ReturnsAsync(new Person { Id = personId });

        // Act
        var result = await _handler.Handle(new DeletePersonCommandRequest(personId), CancellationToken.None);

        // Assert
        _mockPersonRepository.Verify(x => x.Remove(It.IsAny<Person>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        Assert.True(result);
    }
    
    [Fact]
    public async Task Handle_PersonNotFound_ShouldThrowNotFoundException()
    {
        // Arrange
        var personId = 1;

        _mockPersonRepository.Setup(x => x.GetByIdAsync(personId)).ReturnsAsync((Person)null);

        // Act + Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(new DeletePersonCommandRequest(personId), CancellationToken.None));
        _mockPersonRepository.Verify(x => x.Remove(It.IsAny<Person>()), Times.Never);
        _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Never);
    }
}