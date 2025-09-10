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