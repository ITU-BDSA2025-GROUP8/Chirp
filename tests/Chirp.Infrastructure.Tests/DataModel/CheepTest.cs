using System.ComponentModel.DataAnnotations;

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

    [Theory]
   [InlineData("test1","Hello world! This is a short cheep under 160 characters.", true)]
   [InlineData("test2",
       "This cheep has exactly one hundred sixty characters. It is carefully crafted so that the total number of characters in this string adds up to 159",
        true)]
    [InlineData("test3",
        "This cheep is way too long. It exceeds one hundred sixty characters easily. Its purpose is to test how the system handles cheeps that are over the maximum allowed length limit.",
        false)]
    public void constraintLengthOnCheepTest(string name, string input, bool expected)
    {
       
        var author = new Author()
        {
            Name = name,
            EmailAddress = "text@test.dk"
        };

        var cheep = new Cheep
        {
            Author = author,
            Date = new DateTime(2025, 1, 1),
            Text = input
        };

        var validationContext = new ValidationContext(cheep);
        var validationResults = new List<ValidationResult>();
        
        bool result = Validator.TryValidateObject(cheep, validationContext, validationResults, true);
        
        Assert.Equal(result, expected);
    }
    
}