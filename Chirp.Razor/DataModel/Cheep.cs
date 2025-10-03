namespace Chirp.Razor.DataModel;

public class Cheep
{
    public int CheepId { get; set; }
    public string Text { get; set; }
    public DateTime Date { get; set; }
    public Author Author { get; set; }
}