using Microsoft.EntityFrameworkCore;
using Weduka.Domain.Entities;
using Weduka.Domain.Repositories;
using Weduka.Infraestructure.Context;

namespace Weduka.Infraestructure.Respositories;

public class PersonRepository : IPersonRepository
{
    private readonly WedukaContext _context;

    public PersonRepository(WedukaContext context)
    {
        _context = context;
    }

    public void Add(Person? entity)
    {
        _context.Persons.Add(entity);
    }

    public void Update(Person entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Remove(Person? entity)
    {
        _context.Persons.Remove(entity);
    }

    public async Task<Person?> GetByIdAsync(int id)
    {
        return await _context.Persons.Include(x => x.Contacts).FirstOrDefaultAsync(x => x.Id == id);
    }

    public IQueryable<Person> GetAll()
    {
        return  _context.Persons;
    }
}