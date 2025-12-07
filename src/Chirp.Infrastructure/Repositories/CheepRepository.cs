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
    
    // Create a new cheep
    public async Task CreateCheep(CheepDTO newCheep)
    {
        var author = await FindAuthor(newCheep.AuthorId);
        
        if (author == null)
        {
            //Throws an exception since it should always have a valid author, registered in the database - Exception created by ChatGPT
            throw new Exception($"Author with username '{newCheep.AuthorId}' not found."); 
        }
        // Creates new cheep
        Cheep cheep = new()
        {
            Author = author,
            Text = newCheep.Text, 
            Date = newCheep.CreatedAt,
        };
        
        // Adds and saves the cheep in the database
        await _context.Cheeps.AddAsync(cheep);
        await _context.SaveChangesAsync();
    }

    // Read all cheeps
    public async Task<List<CheepDTO>> GetAllCheeps(int? page = null)
    {
        // Construction of query
        var query = (from cheep in _context.Cheeps
            orderby cheep.Date descending
            select new CheepDTO
            {
                Id = cheep.CheepId,
                CreatedAt = cheep.Date, 
                Text = cheep.Text,
                AuthorId = cheep.Author.Name
            }).Skip(GetOffset(page)).Take(32);
        
        // Executing the query
        var result = await query.ToListAsync();
        
        return result;
    }
    
    public async Task<List<CheepDTO>> ReadCheepsBy(string authorName, int? page = null)
    {
        // Construction of the query that selects cheeps written by the authorName
        var query = (from cheep in _context.Cheeps
            where cheep.Author.Name == authorName
            orderby cheep.Date descending
            select new CheepDTO
            {
                Id = cheep.CheepId,
                CreatedAt = cheep.Date,
                Text = cheep.Text,
                AuthorId = cheep.Author.Name
            }).Skip(GetOffset(page)).Take(32);
        
        // Execution of the query
        var result = await query.ToListAsync();
        
        return result;
    }
    
    //Shows all cheeps written by a single author on one page
    public async Task<List<CheepDTO>> ReadCheepsByOnOnePage(string authorName)
    {
        // Construction of the query that selects cheeps written by the authorName
        var query = (from cheep in _context.Cheeps
            where cheep.Author.Name == authorName
            orderby cheep.Date descending
            select new CheepDTO
            {
                Id = cheep.CheepId,
                CreatedAt = cheep.Date,
                Text = cheep.Text,
                AuthorId = cheep.Author.Name
            });
        
        // Execution of the query
        var result = await query.ToListAsync();
        
        return result;
    }
    
    public async Task<List<CheepDTO>> ReadCheepsBySelfAndOthers(IList<string> authorNames, int? page = null)
    {
        // Construction of the query that selects cheeps written by the authorName
        var query = (from cheep in _context.Cheeps
            where authorNames.Contains(cheep.Author.Name)
            orderby cheep.Date descending
            select new CheepDTO
            {
                Id = cheep.CheepId,
                CreatedAt = cheep.Date,
                Text = cheep.Text,
                AuthorId = cheep.Author.Name
            }).Skip(GetOffset(page)).Take(32);
        
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
    
    // Utility method: set the new properties of the Cheep
    private async void UpdateCheep(Cheep originalCheep, CheepDTO alteredCheep)
    {
        // Find the author object of the alteredCheep
        var author = await FindAuthor(alteredCheep.AuthorId);
        
        if (author == null)
        {
            throw new Exception($"Author with username '{alteredCheep.AuthorId}' not found while trying to update cheep.");
        }
        
        // Sets the new properties
        originalCheep.Author = author;
        originalCheep.Text = alteredCheep.Text;
        originalCheep.Date = alteredCheep.CreatedAt;
        originalCheep.CheepId = alteredCheep.Id;
    }
    
    // Utility method: find the author object
    private async Task<Author?> FindAuthor(string AuthorId)
    {
        // Suggestion from ChatGPT
        // Gets one Author object from the database
        var query = from author in _context.Authors
            where  author.Id == AuthorId
            select author;
        var foundAuthor = await query.FirstOrDefaultAsync();

        return foundAuthor;
    }
    //method to get the offset of the cheeps based on what page we want to read from
    private static int GetOffset(int? page)
    {
        int offset = 0; //default offset is 0
        if (page != null && page > 1)
        {
            offset = (page.Value - 1) * 32;
        }

        return offset;
    }
}