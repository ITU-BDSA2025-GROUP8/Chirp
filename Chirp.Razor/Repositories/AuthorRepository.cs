using Chirp.Razor.data;
using Chirp.Razor.DataModel;
using Chirp.Razor.Models;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Razor.Repositories;

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
            //todo: does not set AuthorId
            Name = newUser.Name,
            EmailAddress = newUser.Email,
            Cheeps = new List<Cheep>() //todo: is this to be done in the repository
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
                Name = author.Name,
                Email = author.EmailAddress,
                Cheeps = author.Cheeps
            };

        // Executing the query
        var result = await query.ToListAsync();

        return result;
    }
}