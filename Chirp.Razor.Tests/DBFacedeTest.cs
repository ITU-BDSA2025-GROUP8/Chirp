namespace Chirp.Razor.Test;

public class DBFacedeTest
{
    
    private void Start()
    {
        Environment.SetEnvironmentVariable("CHIRPDBPATH", "chirp.db");
    }
    
    
    [Fact]
    public void CanReadFromEnvironmentVariableTest()
    {
        Start();
        List<CheepViewModel> cheeps = DBFacade.Read();
        Assert.NotNull(cheeps);
    }

    [Fact]
    public void CanGetCorrectAuthorFromDB()
    {
        Start();
        List<CheepViewModel> cheeps;
        cheeps = DBFacade.Read();
        Assert.Equal("Jacqualine Gilcoine",cheeps[0].Author);
    }

    [Fact]
    public void CanGetCorrectMessageFromDB()
    {
        Start();
        List<CheepViewModel> cheeps;
        cheeps =  DBFacade.Read();
        Assert.Equal("Starbuck now is what we hear the worst.", cheeps[0].Message);
    }
    
    [Fact]
    public void CanGetCorrectTimeFromDB()
    {
        Start();
        List<CheepViewModel> cheeps;
        cheeps = DBFacade.Read();
        Assert.Equal("08-01-23 13:17:39",cheeps[0].Timestamp);
    }

    [Fact]
    public void CanGetSpecificAuthorOnly()
    {
        Start();
        List<CheepViewModel> cheeps;
        cheeps = DBFacade.ReadAuthor("Jacqualine Gilcoine");
        foreach (var cheep in cheeps)
        {
            Assert.Equal("Jacqualine Gilcoine", cheep.Author);
        }
    }
    
    [Theory]
    [InlineData("Roger Histand")]
    [InlineData("Luanna Muro")]
    [InlineData("Wendell Ballan")]
    [InlineData("Jacqualine Gilcoine")]
    

    public void OnlyGetSpecificAuthors(String author)
    {
        Start();
        List<CheepViewModel> cheeps;
        cheeps = DBFacade.ReadAuthor(author);
        foreach (var cheep in cheeps)
        {
            Assert.Equal(author, cheep.Author);
        }
    }

}
