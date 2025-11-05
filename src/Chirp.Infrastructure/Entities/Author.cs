using Chirp.Core.DTO;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Chirp.Infrastructure.Entities;

public class Author : IdentityUser 
{
    //IdentityUser already have an ID and email fields
    
    [Required]
    public required string Name { get; set; }
    
    public ICollection<Cheep> Cheeps { get; set; }
}