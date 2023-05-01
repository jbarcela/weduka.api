using MediatR;

namespace Weduka.Application.Commands.Person.Delete;

public class DeletePersonCommandRequest : IRequest<bool>
{
    public int Id { get; set; }
    
    public DeletePersonCommandRequest(int id)
    {
        Id = id;
    }
}