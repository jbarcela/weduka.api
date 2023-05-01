namespace Weduka.Domain.Entities;

public class Contact
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string Value { get; set; }
    public int PersonId { get; set; }
    public Person Person { get; set; }
}