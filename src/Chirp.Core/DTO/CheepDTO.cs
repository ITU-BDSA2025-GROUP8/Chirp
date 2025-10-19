namespace Chirp.Core.DTO;

public class CheepDTO
{
    // Template by ChatGPT:
    // public int Id { get; set; }
    // You could have one for ID but ID should probably be hidden from users
    //code within project can read/write, other code can only read
    public int Id { get; set; } //todo: should it be a internal setter to be 'hidden from users'?
    public string UserName { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}