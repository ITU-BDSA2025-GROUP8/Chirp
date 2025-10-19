using Chirp.Core.DTO;

namespace Chirp.Core.Interfaces;

public interface IAuthorRepository
{
    // Create new author
    Task CreateAuthor(AuthorDTO newUser);
    // Get all authors
    Task<List<AuthorDTO>> GetAllAuthors();
    // Update author
    Task UpdateAuthor(AuthorDTO updatedAuthor);
}