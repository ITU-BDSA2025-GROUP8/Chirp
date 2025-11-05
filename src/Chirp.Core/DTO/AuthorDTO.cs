namespace Chirp.Core.DTO;

public class AuthorDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public ICollection<CheepDTO> Cheeps { get; set; }
}
