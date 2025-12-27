namespace Chirp.Core.DTO;

public class CheepDTO
{
    // Template by ChatGPT:
    // public int Id { get; set; }
    // You could have one for ID but ID should probably be hidden from users
    //code within project can read/write, other code can only read
    public int Id { get; set; } //todo: should it be a internal setter to be 'hidden from users'?
    public required string AuthorId { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public required string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<string> LikedBy { get; set; } = new List<string>();
}