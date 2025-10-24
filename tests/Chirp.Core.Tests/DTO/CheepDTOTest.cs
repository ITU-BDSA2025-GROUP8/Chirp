
using Chirp.Core.DTO;

namespace Chirp.Core.Tests.DTO;

public class CheepDTOTest
{
    [Fact]
    public void CheepDTOStoresAndReturnsAssignedValues()
    {
        var cheepDTO = new CheepDTO();
        cheepDTO.Id = 1;
        cheepDTO.UserName = "Tester";
        cheepDTO.Text = "This is a test";
        cheepDTO.CreatedAt = new DateTime(2025, 1, 1);
        
        Assert.Equal(1, cheepDTO.Id);
        Assert.Equal("Tester", cheepDTO.UserName);
        Assert.Equal("This is a test", cheepDTO.Text);
        Assert.Equal(new DateTime(2025, 1, 1), cheepDTO.CreatedAt);
    }
}