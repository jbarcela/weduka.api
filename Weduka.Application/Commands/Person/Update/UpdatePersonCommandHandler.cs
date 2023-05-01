using MediatR;
using Weduka.Application.Exceptions;
using Weduka.Domain.Entities;
using Weduka.Domain.Interfaces;
using Weduka.Domain.Repositories;

namespace Weduka.Application.Commands.Person.Update;

public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommandRequest, bool>
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePersonCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork)
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdatePersonCommandRequest request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync(request.Id);

        if (person == null)
        {
            throw new NotFoundException("Registro não encontrado");
        }

        person.Name = request.Name;
        person.Contacts = request.Contacts.Select(x => new Contact()
        {
            Type = x.Type,
            Value = x.Value
        }).ToArray();
        
        _personRepository.Update(person);
        await _unitOfWork.CommitAsync();
        
        return true;
    }
}