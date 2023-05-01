using MediatR;
using Weduka.Application.Shared;

namespace Weduka.Application.Commands.Person.Update;

public class UpdatePersonCommandRequest : IRequest<bool>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ContactDTO[] Contacts { get; set; }
}