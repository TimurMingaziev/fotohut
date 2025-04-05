namespace Fotohut.API.Models;

public class Contacts
{
    public int Id { get; set; }
    public ContactType Type { get; set; }
    public required string Value { get; set; }
    public string? CustomType { get; set; }
}