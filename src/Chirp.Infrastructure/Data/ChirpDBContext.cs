using Microsoft.EntityFrameworkCore;
using Chirp.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Chirp.Infrastructure.Data;

// Inherit from IdentityDBContex to enable EF Core Identity (Login functionality)
public class ChirpDBContext : IdentityDbContext<Author>
{
    public ChirpDBContext(DbContextOptions<ChirpDBContext> options) : base(options)
    {
        //empty on purpose
    }
    
    public DbSet<Cheep> Cheeps { get; set; }
    public DbSet<Author> Authors { get; set; }
}