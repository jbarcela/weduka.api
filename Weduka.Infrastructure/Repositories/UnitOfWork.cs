using Weduka.Domain.Interfaces;
using Weduka.Infraestructure.Context;

namespace Weduka.Infraestructure.Respositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly WedukaContext _context;

    public UnitOfWork(WedukaContext context)
    {
        _context = context;
    }
    
    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}