namespace Chirp.Razor.DataModel;

public class Author
{
    public string  Name { get; set; }
    public string  EmailAddress { get; set; }
    public ICollection<Cheep> Cheeps { get; set; }
}