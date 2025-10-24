

using Chirp.Infrastructure.Entities;

namespace Chirp.Infrastructure.Tests.DataModel;

public class CheepTest
{
    /*
     * This test is currently identical to CheepDTOTest, since Cheep and CheepDTO share the same structure.
     * As more functionality is implemented, this test will expand to cover logic as well.
     */
    [Fact]
    public void CheepStoresAndReturnsAssignedValues()
    {
        var cheep = new Cheep{Text = "This is a test"};
        cheep.CheepId = 1;
        var author = new Author
        {
            Name = "Tester",
            AuthorId = 10,
            EmailAddress = "test@itu.dk"
        };
        cheep.Date = new DateTime(2025, 1, 1);
        cheep.Author = author;
        
        Assert.Equal(1, cheep.CheepId);
        Assert.Equal(author, cheep.Author);
        Assert.Equal("This is a test", cheep.Text);
        Assert.Equal(new DateTime(2025, 1, 1), cheep.Date);
    }
}