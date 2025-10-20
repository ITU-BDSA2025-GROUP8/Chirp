using Chirp.Razor.DataModel;

namespace Chirp.Razor.Test.DataModel;

public class CheepTest
{
    /*
     * This test is currently identical to CheepDTOTest, since Cheep and CheepDTO share the same structure.
     * As more functionality is implemented, this test will expand to cover logic as well.
     */
    [Fact]
    public void CheepStoresAndReturnsAssignedValues()
    {
        var author = new Author
        {
            Name = "Tester",
            AuthorId = 10,
            EmailAddress = "test@itu.dk"
        };
        var date = new DateTime(2025, 1, 1);
        var cheep = new Cheep
        {
            CheepId = 1,
            Text = "This is a test",
            Date = date,
            Author = author,
            
        };
        
        Assert.Equal(1, cheep.CheepId);
        Assert.Equal(author, cheep.Author);
        Assert.Equal("This is a test", cheep.Text);
        Assert.Equal(new DateTime(2025, 1, 1), cheep.Date);
    }
}