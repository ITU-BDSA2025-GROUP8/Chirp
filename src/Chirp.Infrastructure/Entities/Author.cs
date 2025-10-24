using Chirp.Core.DTO;
using System.ComponentModel.DataAnnotations;

namespace Chirp.Infrastructure.Entities;

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