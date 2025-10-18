using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Razor.DataModel;

public class Author
{
    [Key]
    public int AuthorId { get; set; }
    
    [Required]
    public required string Name { get; set; }
    
    [Required]
    public required string EmailAddress { get; set; }
    public ICollection<Cheep> Cheeps { get; set; }
}