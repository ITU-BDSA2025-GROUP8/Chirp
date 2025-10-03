namespace Chirp.Razor.Models;

public class CheepDTO
{
    // Template by ChatGPT:
    // public int Id { get; set; }
    // You could have one for ID but ID should probably be hidden from users
    public string UserName { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}