using Chirp.Core.DTO;
using Chirp.Infrastructure.Data;
using Chirp.Infrastructure.Entities;
using Chirp.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure.Tests.Repositories;

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
            var author = new Author { Name = "Test1", EmailAddress = "test1@itu.dk" };

            context.Authors.AddRange(author);
            context.SaveChanges();
            
            var newCheep = new CheepDTO
            {
                UserName = "Test1",
                Text = "I chirped",
                CreatedAt = new DateTime(2025, 10, 8),
            };
            //Act
            repository.CreateCheep(newCheep);

            //Assert
            var numberOfCheeps = repository.GetAllCheeps().Result.Count;
            Assert.Equal(1, numberOfCheeps);
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
                new Cheep { Author = author1, Text = "hi", Date = new DateTime(2025, 10, 2) },
                new Cheep { Author = author2, Text = "hello", Date = new DateTime(2025, 10, 3) },
                new Cheep { Author = author1, Text = "hey", Date = new DateTime(2025, 10, 1) } 
            );
            context.SaveChanges();
        }

        using (var context = new ChirpDBContext(options))
        {
            var repository = new CheepRepository(context);

            //Act
            var allCheeps = repository.GetAllCheeps().Result;

            //Assert
            Assert.Equal(3, allCheeps.Count);
            Assert.Contains(allCheeps, c => c.Text == "hi" && c.UserName == "Test1");
            Assert.Contains(allCheeps, c => c.Text == "hello" && c.UserName == "Test2");
            //Assert.All(allCheeps, c => Assert.True(c.Id > 0)); // Id should exist todo: check if this is needed?
            
            //Assert cheeps are in correct order (newest first)
            Assert.True(allCheeps[0].CreatedAt > allCheeps[1].CreatedAt);
            Assert.True(allCheeps[1].CreatedAt > allCheeps[2].CreatedAt);
            Assert.True(allCheeps[0].CreatedAt > allCheeps[2].CreatedAt);
        }
    }

    [Fact]
    public void ReadCheepsByTest()
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

            var author1 = new Author { Name = "Test1", EmailAddress = "test1@itu.dk" };
            var author2   = new Author { Name = "Test2", EmailAddress = "test2@itu.dk" };
            context.Authors.AddRange(author1, author2);

            //note: setup for these 3 test cheeps was suggested by ChatGPT
            context.Cheeps.AddRange(
                new Cheep { Author = author1, Text = "a1", Date = new DateTime(2025, 10, 10) },
                new Cheep { Author = author1, Text = "a2", Date = new DateTime(2025, 10, 11) },
                new Cheep { Author = author2,   Text = "b1", Date = new DateTime(2025, 10, 12) }
            );
            context.SaveChanges();
        }

        using (var context = new ChirpDBContext(options))
        {
            var repository = new CheepRepository(context);

            var author1Cheeps = repository.ReadCheepsBy("Test1").Result;
            Assert.Equal(2, author1Cheeps.Count);
            Assert.All(author1Cheeps, c => Assert.Equal("Test1", c.UserName));
            //Assert cheeps are in correct order (newest first)
            Assert.True(author1Cheeps[0].CreatedAt > author1Cheeps[1].CreatedAt);
        }
    }

    [Fact]
    public void UpdateCheepTest()
    {
        
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<ChirpDBContext>()
            .UseSqlite(connection)
            .Options;

        int cheepId;

        using (var context = new ChirpDBContext(options))
        {
            context.Database.EnsureCreated();

            var author1 = new Author { Name = "Test1", EmailAddress = "test1@itu.dk" };
            var author2 = new Author { Name = "Test2",   EmailAddress = "test2@itu.dk" };
            context.Authors.AddRange(author1, author2);

            var cheep = new Cheep { Author = author1, Text = "old text", Date = new DateTime(2025, 10, 10) };
            context.Cheeps.Add(cheep);
            context.SaveChanges();

            cheepId = cheep.CheepId;
        }

        using (var context = new ChirpDBContext(options))
        {
            //act
            var repository = new CheepRepository(context);
            var dto = new CheepDTO
            {
                Id = cheepId,
                Text = "altered text",
                CreatedAt = new DateTime(2025, 10, 11),
                UserName = "Test1"
            };

            repository.UpdateCheep(dto);
            //assert a change has happened
            Assert.True(context.Cheeps.Any(c => c.Text == "altered text"));
            //assert new time exists
            Assert.True(context.Cheeps.Any(c => c.Date == new DateTime(2025, 10, 11)));
            //assert old text is gone
            Assert.False(context.Cheeps.Any(c => c.Text == "old text"));
        }
        
    }
    
}