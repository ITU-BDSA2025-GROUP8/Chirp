using Microsoft.EntityFrameworkCore;
using Chirp.Infrastructure.Entities;

namespace Chirp.Infrastructure.Data;

public class ChirpDBContext : DbContext
{
    public ChirpDBContext(DbContextOptions<ChirpDBContext> options) : base(options)
    {
        //empty on un purpose
    }
    
    public DbSet<Cheep> Cheeps { get; set; }
    public DbSet<Author> Authors { get; set; }
}