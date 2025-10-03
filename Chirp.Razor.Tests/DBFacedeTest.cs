namespace Chirp.Razor.Test;

public class DBFacedeTest
{
    private void Start()
    {
        Environment.SetEnvironmentVariable("CHIRPDBPATH", "/tmp/chirp.db");
    }
    
    
    [Fact]
    public void DummyTest()
    {
        Start();
        List<CheepViewModel> cheeps = DBFacade.Read();
        Assert.NotNull(cheeps);
    }
}
