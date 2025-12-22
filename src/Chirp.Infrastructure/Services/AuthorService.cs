using Chirp.Core.DTO;
using Chirp.Core.Interfaces;

namespace Chirp.Infrastructure.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }
    public async Task<AuthorDTO?> GetAuthorByName(string authorUsername)
    {
        return await _authorRepository.FindByName(authorUsername);
    }
    
    public async Task Follow(string thisUsername, string otherUsername)
    {
        AuthorDTO? self = await GetAuthorByName(thisUsername);
        await _authorRepository.FollowUser(self!, otherUsername);
    }

    public async Task Unfollow(string thisUsername, string otherUsername)
    {
        AuthorDTO? self = await GetAuthorByName(thisUsername);
        await _authorRepository.UnFollowUser(self!, otherUsername);
    }

    public async Task DeleteAuthor(string thisUsername)
    {
        AuthorDTO? self = await GetAuthorByName(thisUsername);
        await _authorRepository.DeleteAuthor(self!);
    }
}    