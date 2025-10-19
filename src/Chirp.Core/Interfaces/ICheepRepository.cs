using Chirp.Core.DTO;

namespace Chirp.Core.Interfaces;

public interface ICheepRepository
{
    // Create a new cheep
    Task CreateCheep(CheepDTO newCheep);
    // Read all cheeps
    Task<List<CheepDTO>> GetAllCheeps();
    // Read cheeps by a specific author
    Task<List<CheepDTO>> ReadCheepsBy(string authorName);
    // Update an existing cheep
    Task UpdateCheep(CheepDTO alteredCheep);
}