using Database;

namespace Chirp.CSVDB.Tests;

public class CSVDatabase
{
    [Fact]
    //tests that the database is initialized correctly
    public void CorrectDatabaseInstanceTest()
    {
        var database = CSVDatabase<Cheep>.Instance; //get the instance of the database
        Assert.NotNull(database); //tests that the database is not null
        Assert.IsType<CSVDatabase<Cheep>>(database); //tests that the database is of type CSVDatabase<Cheep>
    }
    
    [Fact]
    //tests that the class returns the correct instance
    public void CorrectInstanceTest()
    {
        var database1 = CSVDatabase<Cheep>.Instance;
        var database2 = CSVDatabase<Cheep>.Instance;
        Assert.Same(database1, database2); //tests that both instances are the same
    }
    
    /*
     * Due to the singleton pattern, the tests above should be enough to test the database
     * as the compiler will not allow multiple instances of the database to be created
     * and therefore can't be tested.
     */
}
