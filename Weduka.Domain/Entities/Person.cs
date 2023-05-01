namespace Weduka.Domain.Entities;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Contact> Contacts { get; set; }
}