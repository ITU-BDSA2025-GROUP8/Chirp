using Chirp.Razor.data;
using Chirp.Razor.DataModel;
using Chirp.Razor.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Razor.Repositories;

public class AuthorRepositoryTest
{

    [Fact]
    public void CreateAuthorTest()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<ChirpDBContext>()
            .UseSqlite(connection)
            .Options;

        using (var context = new ChirpDBContext(options))
        {
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

            var numberOfCheeps = repository.GetAllAuthors().Result.Count;
            Assert.Equal(1, numberOfCheeps);
        }

    }

    [Fact]
    public void GetAllAuthorsTest()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<ChirpDBContext>()
            .UseSqlite(connection)
            .Options;

        using (var context = new ChirpDBContext(options))
        {
            context.Database.EnsureCreated();
            context.Authors.AddRange(
                new Author { AuthorId = 1, Cheeps = new List<Cheep>(), EmailAddress = "test1@itu.dk", Name = "Test1" },
                new Author { AuthorId = 2, Cheeps = new List<Cheep>(), EmailAddress = "test2@itu.dk", Name = "Test2" },
                new Author { AuthorId = 3, Cheeps = new List<Cheep>(), EmailAddress = "test3@itu.dk", Name = "Test3" }
            );
            context.SaveChanges();
        }

        using (var context = new ChirpDBContext(options))
        {
            var repository = new AuthorRepository(context);
            var authors = repository.GetAllAuthors();
            Assert.Equal(3, authors.Result.Count);
            Assert.True(authors.Result.Any(a => a.Id == 1));
            Assert.True(authors.Result.Any(a => a.Id == 2));
            Assert.True(authors.Result.Any(a => a.Id == 3));
        }       
    }

    [Fact]
    public void UpdateAuthorTest()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<ChirpDBContext>()
            .UseSqlite(connection)
            .Options;

        using (var context = new ChirpDBContext(options))
        {
            context.Database.EnsureCreated();
            context.Authors.AddRange(
                new Author { AuthorId = 1, Cheeps = new List<Cheep>(), EmailAddress = "test1@itu.dk", Name = "Test1" },
                new Author { AuthorId = 2, Cheeps = new List<Cheep>(), EmailAddress = "test2@itu.dk", Name = "Test2" }
            );
            context.SaveChanges();
        }

        using (var context = new ChirpDBContext(options))
        {
            var repository = new AuthorRepository(context);
            var authorDTOTest = new AuthorDTO()
            {
                Id = 1,
                Name = "John Doe",
                Email = "test@itu.dk",
                Cheeps = new List<Cheep>()
            };
            
            repository.UpdateAuthor(authorDTOTest);
            
            Assert.True(context.Authors.Any(a => a.Name == "John Doe"));
            Assert.False(context.Authors.Any(a => a.Name == "Test1"));
            Assert.True(context.Authors.Count() == 2);
        }
        
    }
}