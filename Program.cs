//List<string> cheeps = new() { "Hello, BDSA students!", "Welcome to the course!", "I hope you had a good summer.", "Cheeping cheeps on Chirp :)" };
using System.Globalization;

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
                Console.WriteLine(line[Range.EndAt(4)] + " @ " + prettyTime + ": " + message);
            }
        }
    }
    catch (Exception e)
    {
       Console.WriteLine(e);
    }
}

/* goal output:
ropf @ 08/01/23 14:09:20: Hello, BDSA students!
adho @ 08/02/23 14:19:38: Welcome to the course!
adho @ 08/02/23 14:37:38: I hope you had a good summer.
ropf @ 08/02/23 15:04:47: Cheeping cheeps on Chirp :) */