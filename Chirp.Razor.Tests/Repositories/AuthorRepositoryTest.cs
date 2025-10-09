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
                //todo: add ID - not added because of trouble with accessing the setter
                Name = "John Doe",
                Email = "test@itu.dk",
                Cheeps = new List<Cheep>()
            };

            repository.CreateAuthor(authorDTOTest);

            var numberOfCheeps = repository.GetAllAuthors().Result.Count;
            Assert.Equal(1, numberOfCheeps);
        }

    }
}