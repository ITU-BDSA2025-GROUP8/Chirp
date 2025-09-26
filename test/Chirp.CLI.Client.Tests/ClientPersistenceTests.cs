using Database;
namespace Chirp.CLI.Client.Tests;

public class ClientPersistenceTests
{
    [Fact]
    public void ReadFromDBTest()
    {
        var database = CSVDatabase<Cheep>.Instance;

        Cheep test = new Cheep { Author = Environment.UserName, Message = "This is a test", Timestamp = 10000 };

        database.Store(test);  
        IEnumerable<Cheep> cheeps = database.Read();

        Cheep c = null;
        
        foreach (var cheep in cheeps)
        {
            c = cheep;
        }

        Console.WriteLine(c);

        Assert.Equal(c.Author, Environment.UserName);
        Assert.Equal(c.Message, "This is a test");
    }
}
