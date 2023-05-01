using MediatR;
using Weduka.Domain.Entities;
using Weduka.Domain.Interfaces;
using Weduka.Domain.Repositories;

namespace Weduka.Application.Commands.Person.Create;

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommandRequest, CreatePersonCommandResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePersonCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork)
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<CreatePersonCommandResponse> Handle(CreatePersonCommandRequest request, CancellationToken cancellationToken)
    {
        var person = new Domain.Entities.Person()
        {
            Name = request.Name,
            Contacts = request.Contacts.Select(c => new Contact()
            {
                Type = c.Type,
                Value = c.Value
            }).ToArray()
        };

        _personRepository.Add(person);
        await _unitOfWork.CommitAsync();

        return new CreatePersonCommandResponse()
        {
            Id = person.Id,
        };
    }
}