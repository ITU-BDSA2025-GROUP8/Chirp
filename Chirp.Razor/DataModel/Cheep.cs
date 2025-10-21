using System.ComponentModel.DataAnnotations;

namespace Chirp.Razor.DataModel;

public class Cheep
{
    //todo: add required fields
    public int CheepId { get; set; }
    [Required]
    [StringLength(160)]
    public required string Text { get; set; }
    public DateTime Date { get; set; }
    public Author Author { get; set; }
    public int AuthorId { get; set; } //foreign key pointing to the author class
}