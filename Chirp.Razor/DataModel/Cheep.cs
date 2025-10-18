using System.ComponentModel.DataAnnotations;

namespace Chirp.Razor.DataModel;

public class Cheep
{
    [Key]
    public int CheepId { get; set; }
    
    [Required]
    [StringLength(160)]
    public required string Text { get; set; }

    [Required]
    public required DateTime Date { get; set; }

    [Required]
    public required Author Author { get; set; }
}