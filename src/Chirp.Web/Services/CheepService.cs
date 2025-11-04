
using Chirp.Core.DTO;
using Chirp.Core.Interfaces;
using Chirp.Infrastructure.Entities;

namespace Chirp.Web.Services;

public record CheepViewModel(string Author, string Message, string Timestamp);

public interface ICheepService
{
    public List<CheepViewModel> GetCheeps(int? page);
    public List<CheepViewModel> GetCheepsFromAuthor(string author, int? page);
    public void CreateCheepFromDTO(CheepDTO cheep);
}

public class CheepService : ICheepService
{
    private readonly ICheepRepository _cheepRepository;

    public CheepService(ICheepRepository cheepRepository)
    {
        _cheepRepository = cheepRepository;
    }
    
    // Fetches all cheeps by form repository
    public List<CheepViewModel> GetCheeps(int? page = null)
    {
        var cheeps = _cheepRepository.GetAllCheeps(page).Result;

        return cheeps.Select(cheep => new CheepViewModel(Author: cheep.UserName, Message: cheep.Text, Timestamp: cheep.CreatedAt.ToLongDateString())).ToList();
    }

    // Fetches cheeps by specified author form repository
    public List<CheepViewModel> GetCheepsFromAuthor(string author, int? page = null)
    {
        var cheeps = _cheepRepository.ReadCheepsBy(author,page).Result;

        return cheeps.Select(cheep => new CheepViewModel(Author: cheep.UserName, Message: cheep.Text, Timestamp: cheep.CreatedAt.ToLongDateString())).ToList();
    }
    
    // Creates new cheep
    public void CreateCheepFromDTO(CheepDTO cheep)
    {
        _cheepRepository.CreateCheep(cheep);
    }

}