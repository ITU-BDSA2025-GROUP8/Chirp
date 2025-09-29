using Chirp.Razor;

public record CheepViewModel(string Author, string Message, string Timestamp);

public interface ICheepService
{
    public List<CheepViewModel> GetCheeps();
    public List<CheepViewModel> GetCheepsFromAuthor(string author);
}

public class CheepService : ICheepService
{
    // These would normally be loaded from a database for example
    private static List<CheepViewModel> _cheeps = new();

    public List<CheepViewModel> GetCheeps()
    {
        return DBFacade.Read();
    }

    public List<CheepViewModel> GetCheepsFromAuthor(string author)
    {
        // filter by the provided author name
        //return _cheeps.Where(x => x.Author == author).ToList();
        return DBFacade.Read();
    }
    

}