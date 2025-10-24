using Chirp.Core.DTO;
using Chirp.Infrastructure.Entities;

namespace Chirp.Infrastructure.Tests.DataModel;

public class AuthorTest
{
    /*
     * This test is currently identical to AuthorDTOTest, since Author and AuthorDTO share the same structure.
     * As more functionality is implemented, this test will expand to cover logic as well.
     */
    [Fact]
    public void AuthorStoresAndReturnsAssignedValues()
    {
        var author = new Author()
        {
            AuthorId = 1,
            Name = "Test",
            EmailAddress = "test@itu.dk"
            
        };

        var cheeps = new List<Cheep>
        {
            new Cheep
            {
                CheepId = 1,
                Text = "This is a test",
                Date = new DateTime(2025, 1, 1),
                Author = new Author() { Name = "Test", EmailAddress = "test@itu.dk" }
            }
        };
        author.Cheeps = cheeps;
        
        Assert.Equal(1, author.AuthorId);
        Assert.Equal("Test", author.Name);
        Assert.Equal("test@itu.dk", author.EmailAddress);
        Assert.Equal(cheeps, author.Cheeps);
    }
}