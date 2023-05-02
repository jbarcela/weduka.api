namespace Weduka.Application.Shared;

public class PersonDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ContactDTO[] Contacts { get; set; }
}