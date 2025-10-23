using System.Data.Common;
using Chirp.Razor.data;
using Chirp.Razor.DataModel;
using Chirp.Razor.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Razor.Repositories;

public class AuthorRepositoryTest
{
    private readonly DbConnection _connection;
    private readonly DbContextOptions<ChirpDBContext> _options;

    public AuthorRepositoryTest()
    {
        // Make in memory database
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        _options = new DbContextOptionsBuilder<ChirpDBContext>()
            .UseSqlite(_connection)
            .Options;
    }

    ChirpDBContext CreateDbContext() => new ChirpDBContext(_options);

    private void Dispose() => _connection.Dispose();

    [Fact]
    public void CreateAuthorTest()
    {
        // Test the method
        using var context = CreateDbContext();
        context.Database.EnsureCreated();
        var repository = new AuthorRepository(context);

        var authorDTOTest = new AuthorDTO()
        {
            Id = 1,
            Name = "John Doe",
            Email = "test@itu.dk",
            Cheeps = new List<Cheep>()
        };

        repository.CreateAuthor(authorDTOTest);

        // Assert
        var numberOfCheeps = repository.GetAllAuthors().Result.Count;
        Assert.Equal(1, numberOfCheeps);

        //Clean up
        Dispose();
    }

    [Fact]
    public void GetAllAuthorsTest()
    {
        // Add to the database
        using var context = CreateDbContext();
        context.Database.EnsureCreated();
        context.Authors.AddRange(
            new Author { AuthorId = 1, Cheeps = new List<Cheep>(), EmailAddress = "test1@itu.dk", Name = "Test1" },
            new Author { AuthorId = 2, Cheeps = new List<Cheep>(), EmailAddress = "test2@itu.dk", Name = "Test2" },
            new Author { AuthorId = 3, Cheeps = new List<Cheep>(), EmailAddress = "test3@itu.dk", Name = "Test3" }
        );
        context.SaveChanges();

        // Test the method
        var repository = new AuthorRepository(context);
        var authors = repository.GetAllAuthors();

        // Assert
        Assert.Equal(3, authors.Result.Count);
        Assert.True(authors.Result.Any(a => a.Id == 1));
        Assert.True(authors.Result.Any(a => a.Id == 2));
        Assert.True(authors.Result.Any(a => a.Id == 3));

        //Clean up
        Dispose();
    }

    [Fact]
    public void UpdateAuthorTest()
    {
        // Add to the database
        using var context = CreateDbContext();

        context.Database.EnsureCreated();
        context.Authors.AddRange(
            new Author { AuthorId = 1, Cheeps = new List<Cheep>(), EmailAddress = "test1@itu.dk", Name = "Test1" },
            new Author { AuthorId = 2, Cheeps = new List<Cheep>(), EmailAddress = "test2@itu.dk", Name = "Test2" }
        );
        context.SaveChanges();


        // Test the method

        var repository = new AuthorRepository(context);
        var authorDTOTest = new AuthorDTO()
        {
            Id = 1,
            Name = "John Doe",
            Email = "test@itu.dk",
            Cheeps = new List<Cheep>()
        };

        repository.UpdateAuthor(authorDTOTest);

        // Assert
        Assert.True(context.Authors.Any(a => a.Name == "John Doe"));
        var updatedCheep = context.Authors.Find(1);
        Assert.True(updatedCheep.EmailAddress == "test@itu.dk");
        Assert.True(updatedCheep.Cheeps.Count == 0);
        Assert.False(context.Authors.Any(a => a.Name == "Test1"));
        Assert.True(context.Authors.Count() == 2);

        //Clean up
        Dispose();
    }
}