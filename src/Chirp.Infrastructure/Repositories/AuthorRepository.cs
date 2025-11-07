using Chirp.Core.DTO;
using Chirp.Core.Interfaces;
using Chirp.Infrastructure.Data;
using Chirp.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly ChirpDBContext _context;
    public AuthorRepository(ChirpDBContext context)
    {
        _context = context;
    }

    // Create new author
    public async Task CreateAuthor(AuthorDTO newUser)
    {
        // Creates new Author
        Author author = new()
        {
            Name = newUser.Name,
            Email = newUser.Email,
            Cheeps = new List<Cheep>()
        };

        // Adds and saves the cheep in the database
        var queryResult = await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();
    }

    // Get all authors
    public async Task<List<AuthorDTO>> GetAllAuthors()
    {
        // AI helped with syntax problems in this method
        // Construction of query gets all authors
        var authorsQuery = (from author in _context.Authors select author)
            .Include(a => a.Cheeps);
            
        var authors = await authorsQuery.ToListAsync();
        
        var query = from author in authors
            select new AuthorDTO()
            {
                Id = author.Id,
                Name = author.Name,
                Email = author.Email,
                Cheeps = author.Cheeps
                    .Select(c => new CheepDTO
                    {
                        Id = c.CheepId,
                        AuthorId = author.Id,
                        Text = c.Text,
                        CreatedAt = c.Date
                    })
                    .ToList()
            };

        var result =  query.ToList();
        
        return result;
    }

    // Update author
    public async Task UpdateAuthor(AuthorDTO updatedAuthor)
    {
        // Construction of the query that selects cheeps written by the same AuthorID
        var query = from author in _context.Authors
            where author.Id == updatedAuthor.Id 
            select author;
        
        var originalAuthor = await query.FirstOrDefaultAsync();

        // Error handling
        if (originalAuthor == null)
        {
            throw new Exception("Unable to find the cheep");
        }

        // Call to utility method that updates the properties of the original author
        UpdateAuthor(originalAuthor, updatedAuthor);

        // Saves changes
        await _context.SaveChangesAsync();
    }

    // Utility method: set the new properties of the Author
    private void UpdateAuthor(Author originalAuthor, AuthorDTO updatedAuthor)
    {
        // Sets the new properties
        originalAuthor.Id = updatedAuthor.Id;
        originalAuthor.Name = updatedAuthor.Name;
        originalAuthor.Email = updatedAuthor.Email;

        ICollection<Cheep> cheeps = new List<Cheep>();

        foreach (var cheep in updatedAuthor.Cheeps)
        {
            Cheep newCheep = FromCheepDtoToCheep(cheep).Result;
            cheeps.Add(newCheep);
        }
        
        originalAuthor.Cheeps = cheeps;


    }
    
    // Translate from CheepDTO to Cheeps by querying DB
    private async Task<Cheep> FromCheepDtoToCheep(CheepDTO oldCheep)
    {
        var query = from cheep in _context.Cheeps
            where cheep.CheepId == oldCheep.Id
                select cheep;
        var originalCheep = await query.FirstOrDefaultAsync();
        if (originalCheep == null)
        {
            throw new Exception("Unable to find the original cheep");
        }
        return originalCheep;
    }
}