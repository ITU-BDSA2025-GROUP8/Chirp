using Chirp.Razor.data;
using Chirp.Razor.DataModel;
using Chirp.Razor.Models;

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
}