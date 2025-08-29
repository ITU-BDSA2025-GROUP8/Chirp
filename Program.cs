//List<string> cheeps = new() { "Hello, BDSA students!", "Welcome to the course!", "I hope you had a good summer.", "Cheeping cheeps on Chirp :)" };


if (args[0] != "read")
{
    return;
}
try
{
    StreamReader input= new StreamReader(args[1]);
    string line = input.ReadLine();

    while (line != null)
    {
        line = input.ReadLine();
        //time and date
        int unixTime = int.Parse(line[Range.StartAt(line.LastIndexOf(",")+1)]);
        DateTimeOffset date = DateTimeOffset.FromUnixTimeSeconds(unixTime);
        DateTimeOffset localtime =  date.LocalDateTime;
        string prettyTime = localtime.ToString("MM/dd/yy HH:mm:ss").Replace("-", "/");
        
        //message
        Range messageRange = new Range(line.IndexOf(",") + 2, line.LastIndexOf(",") - 1);
        String message = line[messageRange];
        //print
        Console.WriteLine(line[Range.EndAt(4)] + " @ " + prettyTime + ": " + message);
    }
}
catch (FileNotFoundException e)
{
    Console.WriteLine(e.Message);
}

/* goal output:
ropf @ 08/01/23 14:09:20: Hello, BDSA students!
adho @ 08/02/23 14:19:38: Welcome to the course!
adho @ 08/02/23 14:37:38: I hope you had a good summer.
ropf @ 08/02/23 15:04:47: Cheeping cheeps on Chirp :) */