using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Chirp.Infrastructure.Entities;

// Inherit from IdentityUser to enable EF Core Identity (Login functionality)
public class Author : IdentityUser 
{
    // IdentityUser already have an ID and email fields
    
    [Required]
    public required string Name { get; set; }
    public ICollection<Cheep> Cheeps { get; set; } = new List<Cheep>();
    public IList<string> Following { get; set; } = new List<string>();
}