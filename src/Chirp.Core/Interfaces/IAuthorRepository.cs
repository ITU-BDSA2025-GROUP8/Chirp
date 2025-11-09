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
    // Find author by name
    Task<AuthorDTO?> FindByName(string name);
    // Find author by email
    Task<AuthorDTO?> FindByEmail(string email);
}