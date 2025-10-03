using Microsoft.EntityFrameworkCore;
using "../DataModel/Cheep.cs";

namespace Chirp.Razor.data;

public class ChirpDBContext : DbContext
{
    public ChirpDBContext(DbContextOptions<ChirpDBContext> options) : base(options)
    {
        //empty on unpurpose
    }
    
    public Dbset<Cheep> Cheeps { get; set; }
}