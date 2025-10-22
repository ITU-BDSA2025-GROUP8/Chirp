using Chirp.Core.DTO;

namespace Chirp.Infrastructure.Entities;

public class Author
{
    public int AuthorId { get; set; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public ICollection<Cheep> Cheeps { get; set; }
}