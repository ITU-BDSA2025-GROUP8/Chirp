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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Query filter to ensure EF Core never loads cheeps for which IsDeleted is true
        modelBuilder.Entity<Cheep>().HasQueryFilter(c => !c.IsDeleted);
        // Query filter to ensure EF Core never loads authors for which IsDeleted is true
        modelBuilder.Entity<Author>().HasQueryFilter(a => !a.IsDeleted);
    }
}