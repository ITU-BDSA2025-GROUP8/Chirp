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
        
        //Arrange
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
        //Arrange
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<ChirpDBContext>()
            .UseSqlite(connection)
            .Options;

        using (var context = new ChirpDBContext(options))
        {
            context.Database.EnsureCreated();

            //making authors to relate to the cheeps
            var author1 = new Author { Name = "Test1", EmailAddress = "test@itu.dk" };
            var author2 = new Author { Name = "Test2", EmailAddress = "test2@itu.dk" };

            context.Authors.AddRange(author1, author2);
            //making cheeps
            context.Cheeps.AddRange(
                new Cheep { Author = author1, Text = "hi", Date = new DateTime(2025, 10, 1) },
                new Cheep { Author = author2, Text = "hello", Date = new DateTime(2025, 10, 2) }
            );
            context.SaveChanges();
        }

        using (var context = new ChirpDBContext(options))
        {
            var repository = new CheepRepository(context);

            //Act
            var allCheeps = repository.GetAllCheeps().Result;

            //Assert
            Assert.Equal(2, allCheeps.Count);
            Assert.Contains(allCheeps, c => c.Text == "hi" && c.UserName == "Test1");
            Assert.Contains(allCheeps, c => c.Text == "hello" && c.UserName == "Test2");
            //Assert.All(allCheeps, c => Assert.True(c.Id > 0)); // Id should exist todo: check if this is needed?
        }
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