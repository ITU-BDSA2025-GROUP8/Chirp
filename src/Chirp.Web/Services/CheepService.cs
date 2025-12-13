
using Chirp.Core.DTO;
using Chirp.Core.Interfaces;
using Chirp.Infrastructure.Entities;

namespace Chirp.Web.Services;

public record CheepViewModel(string Author, string Message, string Timestamp,List<string> LikedBy);

public interface ICheepService
{
    public List<CheepDTO> GetCheeps(out bool hasNext, int? page);
    public List<CheepDTO> GetCheepsFromAuthor(string author, out bool hasNext, int? page);
    public List<CheepDTO> GetCheepsFromAuthorOnOnePage(string author);
    public Task CreateCheepFromDTO(CheepDTO cheep);
    public List<CheepDTO> GetCheepsFromAuthors(IList<string> authors, out bool hasNext, int? page = null);
    public Task LikeCheep(int cheep,string likedBy);
    public Task UnLikeCheep(int cheep, string likedBy);

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

    // Likes a specific Cheep by its id
    public async Task LikeCheep(int cheepId,string likedBy)
    {
        var cheep = await _cheepRepository.GetCheep(cheepId);
        cheep.LikedBy.Add(likedBy);
        await _cheepRepository.UpdateCheep(cheep);
    }

    // Unlikes a specific cheep by its id
    public async Task UnLikeCheep(int cheepId, string likedBy)
    {
        var cheep = await _cheepRepository.GetCheep(cheepId);
        cheep.LikedBy.Remove(likedBy);
        await _cheepRepository.UpdateCheep(cheep);
    }

}