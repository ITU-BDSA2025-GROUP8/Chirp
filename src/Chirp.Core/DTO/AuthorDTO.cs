namespace Chirp.Core.DTO;

public class AuthorDTO
{
    // public int Id { get; set; }
    // You could have one for ID but ID should probably be hidden from users
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public ICollection<CheepDTO> Cheeps { get; set; }
}