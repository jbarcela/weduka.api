using MediatR;
using Microsoft.EntityFrameworkCore;
using Weduka.Application.Shared;
using Weduka.Domain.Entities;
using Weduka.Domain.Repositories;

namespace Weduka.Application.Queries;

public class GetAllPersonQueryHandler : IRequestHandler<GetAllPersonQueryRequest, GetAllPersonQueryResponse>
{
    private readonly IPersonRepository _personRepository;

    public GetAllPersonQueryHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<GetAllPersonQueryResponse> Handle(GetAllPersonQueryRequest request, CancellationToken cancellationToken)
    {
        var persons = _personRepository.GetAll().Include(x => x.Contacts);
        var response = new GetAllPersonQueryResponse()
        {
            Data = persons.Select(p => new PersonDTO()
            {
                Name = p.Name,
                Contacts = p.Contacts.Select(c => new ContactDTO()
                {
                    Type = c.Type,
                    Value = c.Value
                }).ToArray()
            }).ToArray()
        };
        
        return response;
    }
}