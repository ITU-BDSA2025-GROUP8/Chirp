using Microsoft.EntityFrameworkCore;
using Chirp.Razor.DataModel;

namespace Chirp.Razor.data;

public class ChirpDBContext : DbContext
{
    public ChirpDBContext(DbContextOptions<ChirpDBContext> options) : base(options)
    {
        //empty on un purpose
    }
    
    public DbSet<Cheep> Cheeps { get; set; }
    public DbSet<Author> Authors { get; set; }
}