using System.ComponentModel.DataAnnotations;

namespace Chirp.Infrastructure.Entities;

public class Cheep
{
    [Key]
    public int CheepId { get; set; }
    
    [Required]
    [StringLength(160,  ErrorMessage = "Cheep text can't be longer than 160 characters")]
    public required string Text { get; set; }

    [Required]
    public required DateTime Date { get; set; }

    [Required]
    public required Author Author { get; set; }
    
}