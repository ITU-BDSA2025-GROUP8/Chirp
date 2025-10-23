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
    public async Task CreateAuthorTest()
    {
        // Test the method
        using var context = CreateDbContext();
        context.Database.EnsureCreated();
        var repository = new AuthorRepository(context);

        var authorDtoTest = new AuthorDTO()
        {
            Id = 1,
            Name = "John Doe",
            Email = "test@itu.dk",
            Cheeps = new List<Cheep>()
        };

        await repository.CreateAuthor(authorDtoTest);

        // Assert
        var cheeps = await repository.GetAllAuthors();
        var numberOfCheeps = cheeps.Count();
        Assert.Equal(1, numberOfCheeps);

        //Clean up
        Dispose();
    }

    [Fact]
    public async Task GetAllAuthorsTest()
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
        var authors = await repository.GetAllAuthors();

        // Assert
        Assert.Equal(3, authors.Count);
        Assert.Contains(authors, a => a.Id == 1);
        Assert.Contains(authors, a => a.Id == 2);
        Assert.Contains(authors, a => a.Id == 3);

        //Clean up
        Dispose();
    }

    [Fact]
    public async Task UpdateAuthorTest()
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
        var authorDtoTest = new AuthorDTO()
        {
            Id = 1,
            Name = "John Doe",
            Email = "test@itu.dk",
            Cheeps = new List<Cheep>()
        };

        await repository.UpdateAuthor(authorDtoTest);

        // Assert
        Assert.True(context.Authors.Any(a => a.Name == "John Doe"));
        var queryToFindUpdatedAuthor = from Author in context.Authors
            where Author.AuthorId == 1
            select Author;
        var updatedAuthor = queryToFindUpdatedAuthor.Single();
        Assert.NotNull(updatedAuthor);
        Assert.Equal("test@itu.dk", updatedAuthor.EmailAddress);
        Assert.True(updatedAuthor.Cheeps.Count == 0);
        Assert.False(context.Authors.Any(a => a.Name == "Test1"));
        Assert.True(context.Authors.Count() == 2);

        //Clean up
        Dispose();
    }
}