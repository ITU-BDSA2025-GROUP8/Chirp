using Chirp.Razor.data;
using Chirp.Razor.DataModel;
using Chirp.Razor.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Razor.Repositories;

// Use chapter 36.4 in the book as reference. PDF page 1785

public class CheepRepositoryTest
{
    
    [Fact]
    public void CreateCheepTest()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<ChirpDBContext>()
            .UseSqlite(connection)
            .Options;

        using (var context = new ChirpDBContext(options))
        {
            context.Database.EnsureCreated(); 
            var repository = new CheepRepository(context);
            var newCheep = new CheepDTO
            {
                UserName = "Jasmine",
                Text = "I chirped",
                CreatedAt = new DateTime(2025, 10, 8, 0, 0, 0, 0),
            };
            repository.CreateCheep(newCheep);
            //Assert.Equal(4, context.Cheeps.Count());

            var numberOfCheeps = repository.GetAllCheeps().Result.Count;
            Assert.Equal(1, numberOfCheeps);

            //todo: should probably check for some more things
        }
    }

    [Fact]
    public void GetAllCheepsTest()
    {
        
    }

    [Fact]
    public void ReadCheepsByTest()
    {
        
    }

    [Fact]
    public void UpdateCheepTest()
    {
        
    }
    
}