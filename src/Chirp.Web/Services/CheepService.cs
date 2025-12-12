
using Chirp.Core.DTO;
using Chirp.Core.Interfaces;
using Chirp.Infrastructure.Entities;

namespace Chirp.Web.Services;

public record CheepViewModel(string Author, string Message, string Timestamp);

public interface ICheepService
{
    public List<CheepDTO> GetCheeps(out bool hasNext, int? page);
    public List<CheepDTO> GetCheepsFromAuthor(string author, out bool hasNext, int? page);
    public List<CheepDTO> GetCheepsFromAuthorOnOnePage(string author);
    public Task CreateCheepFromDTO(CheepDTO cheep);
    public List<CheepDTO> GetCheepsFromAuthors(IList<string> authors, out bool hasNext, int? page = null);

}

public class CheepService : ICheepService
{
    private readonly ICheepRepository _cheepRepository;

    public CheepService(ICheepRepository cheepRepository)
    {
        _cheepRepository = cheepRepository;
    }
    
    // Fetches all cheeps by form repository
    public List<CheepDTO> GetCheeps(out bool hasNext, int? page = null)
    {
        var cheeps = _cheepRepository.GetAllCheeps(page).Result;

        hasNext = cheeps.Count() == 32;
        
        return cheeps;
    }

    // Fetches cheeps by specified author form repository
    public List<CheepDTO> GetCheepsFromAuthor(string author, out bool hasNext, int? page = null)
    {
        var cheeps = _cheepRepository.ReadCheepsBy(author,page).Result;

        hasNext = cheeps.Count() == 32;

        return cheeps;
    }
    
    // Festches cheeps by specified author from repository on one page
    public List<CheepDTO> GetCheepsFromAuthorOnOnePage(string author)
    {
        var cheeps = _cheepRepository.ReadCheepsByOnOnePage(author).Result;

        return cheeps;
    }
    
    // Fetches cheeps by specified authors form repository
    public List<CheepDTO> GetCheepsFromAuthors(IList<string> authors, out bool hasNext, int? page = null)
    {
        var cheeps = _cheepRepository.ReadCheepsBySelfAndOthers(authors,page).Result;
        
        hasNext = cheeps.Count() == 32;

        return cheeps;
    }
    
    // Creates new cheep
    public async Task CreateCheepFromDTO(CheepDTO cheep)
    {
        await _cheepRepository.CreateCheep(cheep);
    }

}