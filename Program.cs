using Chirp.CLI.UserInterface;
using SimpleDB;

var database = new CSVDatabase<Cheep>();

if (args[0] == "read")
{
    try
    {
        // Print all cheeps
        UserInterface.printCheeps(database.Read());
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
    
} else if (args[0] == "cheep")
{
    try
    {
        long time = DateTimeOffset.Now.ToUnixTimeSeconds();
        Cheep cheep = new Cheep { Author = Environment.UserName, Message = args[1], Timestamp = time};
        database.Store(cheep);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
}

else
{
    Console.WriteLine("Command not recognized");
}

