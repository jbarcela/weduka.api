namespace Weduka.Domain.Interfaces;

public interface IUnitOfWork
{
    Task CommitAsync();
}