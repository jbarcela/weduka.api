using MediatR;
using Weduka.Application.Exceptions;
using Weduka.Domain.Interfaces;
using Weduka.Domain.Repositories;

namespace Weduka.Application.Commands.Person.Delete;

public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommandRequest, bool>
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePersonCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork)
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeletePersonCommandRequest request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync(request.Id);

        if (person == null)
        {
            throw new NotFoundException("Registro não encontrado");
        }
        
        _personRepository.Remove(person);
        _unitOfWork.CommitAsync();

        return true;
    }
}