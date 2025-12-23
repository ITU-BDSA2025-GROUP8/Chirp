using Chirp.Core.DTO;

namespace Chirp.Core.Interfaces;

public interface ICheepRepository
{
    Task CreateCheep(CheepDTO newCheep);
    Task<List<CheepDTO>> GetAllCheeps(int? page = null);
    Task<List<CheepDTO>> ReadCheepsBy(string authorName, int? page = null);
    Task<List<CheepDTO>> ReadCheepsByOnOnePage(string authorName);
    Task<List<CheepDTO>> ReadCheepsBySelfAndOthers(IList<string> authorNames, int? page = null);
    Task UpdateCheep(CheepDTO alteredCheep);
    Task<CheepDTO> GetCheep(int id);
}