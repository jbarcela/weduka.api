using MediatR;
using Weduka.Application.Exceptions;
using Weduka.Application.Shared;
using Weduka.Domain.Repositories;

namespace Weduka.Application.Queries.Person.GetById;

public class GetByIdPersonQueryHandler : IRequestHandler<GetByIdPersonQueryRequest, GetByIdPersonQueryResponse>
{
    private readonly IPersonRepository _personRepository;

    public GetByIdPersonQueryHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<GetByIdPersonQueryResponse> Handle(GetByIdPersonQueryRequest request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync(request.Id);
        
        if (person == null)
        {
            throw new NotFoundException("Registro não encontrado");
        }

        var response = new GetByIdPersonQueryResponse()
        {
            Data = new PersonDTO()
            {
                Name = person.Name,
                Contacts = person.Contacts.Select(c => new ContactDTO()
                {
                    Type = c.Type,
                    Value = c.Value
                }).ToArray()
            }
        };

        return response;
    }
}