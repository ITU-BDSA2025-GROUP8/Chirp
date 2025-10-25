using System.Collections;
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
            EmailAddress = newUser.Email,
            Cheeps = new List<Cheep>()
        };

        // Adds and saves the cheep in the database
        var queryResult = await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();
    }

    // Get all authors
    public async Task<List<AuthorDTO>> GetAllAuthors()
    {
        // Construction of query
        var query = from author in _context.Authors
            select new AuthorDTO()
            {
                Id = author.AuthorId,
                Name = author.Name,
                Email = author.EmailAddress,
                Cheeps = author.Cheeps
                    .Select(c => new CheepDTO
                    {
                        Id = c.CheepId,
                        Text = c.Text,
                        CreatedAt = c.Date
                    })
                    .ToList()
            };

        // Executing the query
        var result = await query.ToListAsync();

        return result;
    }

    // Update author
    public async Task UpdateAuthor(AuthorDTO updatedAuthor)
    {
        // Construction of the query that selects cheeps written by the same AuthorID
        var query = from author in _context.Authors
            where author.AuthorId == updatedAuthor.Id 
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
        originalAuthor.AuthorId = updatedAuthor.Id;
        originalAuthor.Name = updatedAuthor.Name;
        originalAuthor.EmailAddress = updatedAuthor.Email;

        ICollection<Cheep> cheeps = new List<Cheep>();

        foreach (var cheep in updatedAuthor.Cheeps)
        {
            Cheep newCheep = FromCheepDtoToCheep(cheep).Result;
            cheeps.Add(newCheep);
        }
        
        originalAuthor.Cheeps = cheeps;


    }

    private async Task<Cheep> FromCheepDtoToCheep(CheepDTO oldCheep)
    {
        var query = from cheep in _context.Cheeps
            where cheep.CheepId == oldCheep.Id
                select cheep;
        var originalCheep = await query.FirstOrDefaultAsync();
        return originalCheep;
    }
}