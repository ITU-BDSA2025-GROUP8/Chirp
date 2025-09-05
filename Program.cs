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

static void Reader()
{
    // csvHelper formatting
    /*var config = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        PrepareHeaderForMatch = args => args.Header.ToLower(), //controls how properties match against header names
    };*/
    using (var reader = new StreamReader("chirp_cli_db.csv"))
    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
    {
        var records = csv.GetRecords<Cheep>();
        foreach (var cheep in records)
        {
            //time and date
            long unixTime = cheep.Timestamp;
            DateTimeOffset date = DateTimeOffset.FromUnixTimeSeconds(unixTime);
            DateTimeOffset localtime = date.LocalDateTime;
            string prettyTime = localtime.ToString("MM/dd/yy HH:mm:ss").Replace("-", "/");
                
            //print
            Console.WriteLine(cheep.ToString());
        }
        
    }

    /*foreach (Cheep cheep in records){
        
        
            
                
        //print
        Console.WriteLine(cheep.author + " @ " + prettyTime + ": " + cheep.message);
            
    }*/
}

static void Writer(string args)
{
    using (StreamWriter sw = File.AppendText("chirp_cli_db.csv"))
    {
        //tager ikke højde for beskeder med citationstegn inde i beskeden
        //time
        DateTimeOffset now = DateTime.Now;
        
        //write
        sw.WriteLine(Environment.UserName + "," + '"' + args + '"' + "," + now.ToUnixTimeSeconds());
    }

}

// Cheep record
public record Cheep
{
    public string Author { get; set; }
    public string Message { get; set; }
    public long Timestamp { get; set; }
    
    public override string ToString()
    {
        return Author + " @ " + PrettyTime() + ": " + Message;;
    }
    /* Format:
    ropf @ 08/01/23 14:09:20: Hello, BDSA students!
    adho @ 08/02/23 14:19:38: Welcome to the course!
    adho @ 08/02/23 14:37:38: I hope you had a good summer.
    ropf @ 08/02/23 15:04:47: Cheeping cheeps on Chirp :)
    */
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

