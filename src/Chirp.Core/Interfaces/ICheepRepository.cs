using Chirp.Core.DTO;

namespace Chirp.Core.Interfaces;

public interface ICheepRepository
{
    // Create a new cheep
    Task CreateCheep(CheepDTO newCheep);
    // Read all cheeps
    Task<List<CheepDTO>> GetAllCheeps(int? page = null);
    // Read cheeps by a specific author
    Task<List<CheepDTO>> ReadCheepsBy(string authorName, int? page = null);
    // Read cheeps by yourself and given authors
    Task<List<CheepDTO>> ReadCheepsBySelfAndOthers(IList<string> authorNames, int? page = null);
    // Update an existing cheep
    Task UpdateCheep(CheepDTO alteredCheep);
}