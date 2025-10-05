using Chirp.Razor.data;
using Chirp.Razor.DataModel;
using Chirp.Razor.Models;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Razor.Repositories;

public class CheepRepository : ICheepRepository
{
    // Instance of the database context is required for writing queries with EF Core
    private readonly ChirpDBContext  _context; 

    public CheepRepository(ChirpDBContext context)
    {
        _context = context;
    }
    
    // Create a new cheep todo: not run or tested
    public async Task CreateCheep(CheepDTO newCheep)
    {
        // Suggestion from ChatGPT
        // Gets one Author object from the database
        var author = await (
            from a in _context.Authors
            where a.Name == newCheep.UserName 
            select a
            ).FirstOrDefaultAsync(); // Runs the query
        
        if (author == null)
        {
            //todo: call method to create new author instead of throw exception
            //for now throw expection - written by ChatGPT
            throw new Exception($"Author with username '{newCheep.UserName}' not found.");
        }
        // Creates new cheep
        Cheep cheep = new()
        {
            Author = author,
            Text = newCheep.Text, 
            Date = newCheep.CreatedAt,
        };
        
        // Adds and saves the cheep in the database
        var queryResult = await _context.Cheeps.AddAsync(cheep);
        await _context.SaveChangesAsync();
    }

    public async Task<List<CheepDTO>> GetAllCheeps()
    {
        var query = from cheep in _context.Cheeps
            select new CheepDTO
            {
                CreatedAt = cheep.Date, 
                Text = cheep.Text,
                UserName = cheep.Author.Name
            };
        var result = await query.ToListAsync();
        return result;
    }

    public async Task<List<CheepDTO>> ReadCheepsBy(string authorName)
    {
        var query = from cheep in _context.Cheeps
            where cheep.Author.Name == authorName
            select new CheepDTO
            {
                CreatedAt = cheep.Date,
                Text = cheep.Text,
                UserName = cheep.Author.Name
            };
        var result = await query.ToListAsync();
        return result;
    }
}