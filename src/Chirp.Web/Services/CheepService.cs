using Chirp.Web;

public record CheepViewModel(string Author, string Message, string Timestamp);

public interface ICheepService
{
    public List<CheepViewModel> GetCheeps(int? page);
    public List<CheepViewModel> GetCheepsFromAuthor(string author, int? page);
}

public class CheepService : ICheepService
{
    
    //Calls DBFacade to get all cheeps
    public List<CheepViewModel> GetCheeps(int? page = null)
    {
        return DBFacade.Read(page);
    }

    //Calls DBFacade to get cheeps from a specific author
    public List<CheepViewModel> GetCheepsFromAuthor(string author, int? page = null)
    {
        return DBFacade.ReadAuthor(author, page);
    }
    

}