namespace Weduka.Domain.Repositories;

public interface IRepository<T> where T : class
{
    void Add(T entity);
    void Update(T entity);
    void Remove(T entity);
    Task<T> GetByIdAsync(int id);
    IQueryable<T> GetAll();
}