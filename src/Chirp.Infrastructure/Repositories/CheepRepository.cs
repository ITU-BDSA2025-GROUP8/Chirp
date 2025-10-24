using Chirp.Core.DTO;
using Chirp.Core.Interfaces;
using Chirp.Infrastructure.Data;
using Chirp.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure.Repositories;

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
        var author = await findAuthor(newCheep.UserName);
        
        if (author == null)
        {
            //todo: call method to create new author instead of throw exception
            //for now throw exception - written by ChatGPT
            throw new Exception($"Author with username '{newCheep.UserName}' not found.");
        }
        // Creates new cheep
        Cheep cheep = new()
        {
            Author = author,
            Text = newCheep.Text, 
            Date = newCheep.CreatedAt,
            //todo: does not set the cheep ID
        };
        
        // Adds and saves the cheep in the database
        var queryResult = await _context.Cheeps.AddAsync(cheep);
        await _context.SaveChangesAsync();
    }

    // Read all cheeps
    public async Task<List<CheepDTO>> GetAllCheeps()
    {
        // Construction of query
        var query = from cheep in _context.Cheeps
            orderby cheep.Date descending
            select new CheepDTO
            {
                CreatedAt = cheep.Date, 
                Text = cheep.Text,
                UserName = cheep.Author.Name
            };
        
        // Executing the query
        var result = await query.ToListAsync();
        
        return result;
    }

    public async Task<List<CheepDTO>> ReadCheepsBy(string authorName)
    {
        // Construction of the query that selects cheeps written by the authorName //todo: should be changed to the author's ID
        var query = from cheep in _context.Cheeps
            where cheep.Author.Name == authorName
            orderby cheep.Date descending
            select new CheepDTO
            {
                CreatedAt = cheep.Date,
                Text = cheep.Text,
                UserName = cheep.Author.Name
            };
        
        // Execution of the query
        var result = await query.ToListAsync();
        
        return result;
    }

    public async Task UpdateCheep(CheepDTO alteredCheep)
    {
        // Selects the original cheep from the database
        var query = from cheep in _context.Cheeps
            where cheep.CheepId == alteredCheep.Id
            select cheep;
        
        var originalCheep = await query.FirstOrDefaultAsync();
        
        // Error handling
        if (originalCheep == null)
        {
            throw new Exception("Unable to find the cheep");
        }
        
        // Call to utility method that updates the properties of the original cheep
        UpdateCheep(originalCheep, alteredCheep);
        
        // Saves changes
        await _context.SaveChangesAsync();
    }

    // Utility methods todo: should they be in the repository?
    
    // Utility method: set the new properties of the Cheep
    private async void UpdateCheep(Cheep originalCheep, CheepDTO alteredCheep)
    {
        // Find the author object of the alteredCheep
        var author = await findAuthor(alteredCheep.UserName);
        
        if (author == null)
        {
            throw new Exception($"Author with username '{alteredCheep.UserName}' not found while trying to update cheep.");
        }
        
        // Sets the new properties
        originalCheep.Author = author;
        originalCheep.Text = alteredCheep.Text;
        originalCheep.Date = alteredCheep.CreatedAt;
        originalCheep.CheepId = alteredCheep.Id;
    }
    
    // Utility method: find the author object
    //todo: right now the author object is found by the name - this should be changed to the ID as this is the key of an author object in the data model
    private async Task<Author> findAuthor(string AuthorName)
    {
        // Suggestion from ChatGPT
        // Gets one Author object from the database
        var author = await(
            from a in _context.Authors
            where a.Name == AuthorName
            select a
        ).FirstOrDefaultAsync(); // Runs the query and return the first author with that name or default value 

        return author;
    }
}