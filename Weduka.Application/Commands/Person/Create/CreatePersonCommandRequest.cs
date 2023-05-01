using MediatR;
using Weduka.Application.Shared;
using Weduka.Domain.Entities;

namespace Weduka.Application.Commands.Person.Create;

public class CreatePersonCommandRequest : IRequest<CreatePersonCommandResponse>
{
    public string Name { get; set; }
    public ContactDTO[] Contacts { get; set; }
}