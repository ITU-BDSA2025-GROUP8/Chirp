namespace Chirp.Core.DTO;

public class AuthorDTO
{
    // public int Id { get; set; }
    // You could have one for ID but ID should probably be hidden from users
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public ICollection<CheepDTO> Cheeps { get; set; } = new List<CheepDTO>();
}