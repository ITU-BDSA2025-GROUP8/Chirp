
using Chirp.Core.DTO;
using Chirp.Core.Interfaces;
using Chirp.Infrastructure.Entities;

namespace Chirp.Web.Services;

public record CheepViewModel(string Author, string Message, string Timestamp);

public interface ICheepService
{
    public List<CheepViewModel> GetCheeps(int? page);
    public List<CheepViewModel> GetCheepsFromAuthor(string author, int? page);
    public Task CreateCheepFromDTO(CheepDTO cheep);
    public List<CheepViewModel> GetCheepsFromAuthors(IList<string> authors, int? page = null);

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

        return cheeps.Select(cheep => new CheepViewModel(Author: cheep.AuthorId, Message: cheep.Text, Timestamp: cheep.CreatedAt.ToLongDateString())).ToList();
    }

    // Fetches cheeps by specified author form repository
    public List<CheepViewModel> GetCheepsFromAuthor(string author, int? page = null)
    {
        var cheeps = _cheepRepository.ReadCheepsBy(author,page).Result;

        return cheeps.Select(cheep => new CheepViewModel(Author: cheep.AuthorId, Message: cheep.Text, Timestamp: cheep.CreatedAt.ToLongDateString())).ToList();
    }
    
    // Fetches cheeps by specified authors form repository
    public List<CheepViewModel> GetCheepsFromAuthors(IList<string> authors, int? page = null)
    {
        var cheeps = _cheepRepository.ReadCheepsBySelfAndOthers(authors,page).Result;
        return cheeps.Select(cheep => new CheepViewModel(Author: cheep.AuthorId, Message:cheep.Text, Timestamp:cheep.CreatedAt.ToLongDateString())).ToList();
    }
    
    // Creates new cheep
    public async Task CreateCheepFromDTO(CheepDTO cheep)
    {
        await _cheepRepository.CreateCheep(cheep);
    }

}