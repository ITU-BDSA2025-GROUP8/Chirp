using SimpleDB;

var database = new CSVDatabase<Cheep>();

if (args[0] == "read")
{
    try
    {
        var cheeps = database.Read();
        // Print all cheeps
        foreach (var cheep in cheeps)
        {
            Console.WriteLine(cheep.ToString());
        }
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

// Cheep record consisting of author, message and timestamp
public record Cheep
{
    public string Author { get; set; }
    public string Message { get; set; }
    public long Timestamp { get; set; }
    
    // ToString method. Format:
    // ropf @ 08/01/23 14:09:20: Hello, BDSA students!
    public override string ToString()
    {
        return Author + " @ " + PrettyTime() + ": " + Message;
    }
   
    // Convert and format timestamp
    string PrettyTime()
    {
        //time and date
        long unixTime = Timestamp;
        DateTimeOffset date = DateTimeOffset.FromUnixTimeSeconds(unixTime);
        DateTimeOffset localtime = date.LocalDateTime;
        string prettyTime = localtime.ToString("MM/dd/yy HH:mm:ss").Replace("-", "/");
        return prettyTime;
    }
    
}