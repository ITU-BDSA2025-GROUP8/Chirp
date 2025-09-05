if (args[0] == "read")
{
    try
    {
        Reader(args[1]);
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

static void Reader(string args)
{
    try
    {
        StreamReader input = new StreamReader(args);
        input.ReadLine(); //skips first line

        while (input.EndOfStream == false)
        {
            String line = input.ReadLine();
            
            if (line != null)
            {
                //time and date
                int index = line.LastIndexOf(',');
                string time = line.Substring(index+1);
                int unixTime = int.Parse(time);
                DateTimeOffset date = DateTimeOffset.FromUnixTimeSeconds(unixTime);
                DateTimeOffset localtime = date.LocalDateTime;
                string prettyTime = localtime.ToString("MM/dd/yy HH:mm:ss").Replace("-", "/");

                //message
                Range messageRange = new Range(line.IndexOf(",") + 2, line.LastIndexOf(",") - 1);
                String message = line[messageRange];
                
                //print
                Console.WriteLine(line.Substring(0, line.IndexOf(",")) + " @ " + prettyTime + ": " + message);
            }
        }
    }
    catch (Exception e)
    {
       Console.WriteLine(e);
    }
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