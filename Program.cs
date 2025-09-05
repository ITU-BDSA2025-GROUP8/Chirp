using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

if (args[0] == "read")
{
    try
    {
        Reader();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
    
} else if (args[0] == "cheep")
{
    try
    {
        Writer(args[1]);
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
//Reads all records from CSV-file using CsvHelper
//Maps each line to an object and prints them in the console
static void Reader()
{
    using (var reader = new StreamReader("chirp_cli_db.csv"))
    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
    {
        var records = csv.GetRecords<Cheep>();
        foreach (var cheep in records)
        {
            //print
            Console.WriteLine(cheep.ToString());
        }
        
    }
}

//Adds a new Cheep to a specified csv file 
static void Writer(string args)
{
    using var writer = new StreamWriter("chirp_cli_db.csv", true);
    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    {
        long time = DateTimeOffset.Now.ToUnixTimeSeconds();

        Cheep cheep = new Cheep { Author = Environment.UserName, Message = args, Timestamp = time};
        
        //write all records to the chirp_cli_db.csv file
        csv.WriteRecord(cheep);
        csv.NextRecord();
        Console.WriteLine("Cheeped: " + cheep);
    }

}

// Cheep record consisting of author, message and timestamp
public record Cheep
{
    public string Author { get; set; }
    public string Message { get; set; }
    public long Timestamp { get; set; }
    
    //returns a string with author, time and message formatted like the following: ropf @ 08/01/23 14:09:20: Hello, BDSA students!
    public override string ToString()
    {
        return Author + " @ " + PrettyTime() + ": " + Message;
    }
   
    //converts the timestamp to the intended format
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