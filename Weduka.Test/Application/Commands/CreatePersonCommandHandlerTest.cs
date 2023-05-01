using Moq;
using Weduka.Application.Commands.Person.Create;
using Weduka.Application.Shared;
using Weduka.Domain.Entities;
using Weduka.Domain.Interfaces;
using Weduka.Domain.Repositories;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace Weduka.Test.Application.Commands;

public class CreatePersonCommandHandlerTest
{
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreatePersonCommandHandler _handler;
    
    public CreatePersonCommandHandlerTest()
    {
        _personRepositoryMock = new Mock<IPersonRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new CreatePersonCommandHandler(_personRepositoryMock.Object, _unitOfWorkMock.Object);
    }
    
    [Fact]
    public async Task Handle_WithValidRequest_ShouldAddPersonAndReturnResponse()
    {
        // Arrange
        var request = new CreatePersonCommandRequest
        {
            Name = "Person 1",
            Contacts = new ContactDTO[]
            {
                new()
                {
                    Type = "phone", 
                    Value = "51982237199"
                }, 
                new()
                {
                    Type = "email", 
                    Value = "jrbarcela@outlook.com"
                }
            }
        };

        _personRepositoryMock
            .Setup(x => x.Add(It.IsAny<Person>()))
            .Callback<Person>(p => p.Id = 1);

        _unitOfWorkMock
            .Setup(x => x.CommitAsync())
            .Returns(Task.CompletedTask);

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        _personRepositoryMock.Verify(x => x.Add(It.IsAny<Person>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
        Assert.AreEqual(1, response.Id);
    }
}