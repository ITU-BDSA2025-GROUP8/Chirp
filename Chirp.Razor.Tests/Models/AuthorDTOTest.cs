using Chirp.Razor.DataModel;
using Chirp.Razor.Models;

namespace Chirp.Razor.Test.Models;

public class AuthorDTOTest
{
    [Fact]
    public void AuthorDTOStoresAndReturnsAssignedValues()
    {
        var authorDTO = new AuthorDTO();
        authorDTO.Id = 1;
        authorDTO.Name = "Test";
        authorDTO.Email = "test@itu.dk";

        var cheeps = new List<Cheep> { new Cheep { Text = "test" } };
        authorDTO.Cheeps = cheeps;
        
        Assert.Equal(1, authorDTO.Id);
        Assert.Equal("Test", authorDTO.Name);
        Assert.Equal("test@itu.dk", authorDTO.Email);
        Assert.Equal(cheeps, authorDTO.Cheeps);
    }
}