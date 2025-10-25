using Chirp.Core.DTO;

namespace Chirp.Core.Tests.DTO;

public class AuthorDTOTest
{
    [Fact]
    public void AuthorDTOStoresAndReturnsAssignedValues()
    {
        var authorDTO = new AuthorDTO();
        authorDTO.Id = 1;
        authorDTO.Name = "Test";
        authorDTO.Email = "test@itu.dk";

        var cheeps = new List<CheepDTO> { new CheepDTO { Text = "test" } };
        authorDTO.Cheeps = cheeps;
        
        Assert.Equal(1, authorDTO.Id);
        Assert.Equal("Test", authorDTO.Name);
        Assert.Equal("test@itu.dk", authorDTO.Email);
        Assert.Equal(cheeps, authorDTO.Cheeps);
    }
}