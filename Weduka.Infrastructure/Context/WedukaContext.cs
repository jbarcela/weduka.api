using Microsoft.EntityFrameworkCore;
using Weduka.Domain.Entities;

namespace Weduka.Infraestructure.Context;

public class WedukaContext : DbContext
{
    public DbSet<Person?> Persons { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseNpgsql("Server=weduka-database.cxxyyey3jkxo.sa-east-1.rds.amazonaws.com;Port=5432;Database=weduka-database;User Id=postgres;Password=Weduka123;");
}