using Chirp.Razor.Models;

namespace Chirp.Razor.Repositories;

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
    // Get a paginated list of cheeps
    Task<List<CheepDTO>> GetCheepsPage(int pageNumber, int pageSize);
    //Get a paginated list of cheeps from a specific author
    Task<List<CheepDTO>> GetCheepsPageByAuthor(string authorName, int pageNumber, int pageSize);
    //todo: maybe add CreateCheep if cheep is from an unknown author?
}