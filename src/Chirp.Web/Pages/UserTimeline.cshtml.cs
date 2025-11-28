using Chirp.Infrastructure.Entities;
using Chirp.Web.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Chirp.Web.Services;
using Microsoft.AspNetCore.Identity;

namespace Chirp.Web.Pages;

//Pages for cheeps from a specific author
public class UserTimelineModel : TimelineBaseModel
{
    [BindProperty]
    public string? UserName { get; set; }
    
    //Inherits from parent class TimelineBaseModel, which injects the cheep service and sets a model
    public UserTimelineModel(ICheepService cheepService,IAuthorService authorService, UserManager<Author> userManager) : base(cheepService,authorService, userManager)
    {
    }
    
    // Used for page links
    public int PageNumber { get; set; }
    public bool HasMorePages { get; set; }

    //Gets all cheeps from a specific author
    public async Task<ActionResult> OnGet(string author, [FromQuery] string? error, [FromQuery] int page = 1)
    {
        HandleError(error);
        
        //Call base method to get user info
        await GetUserInformation();
        
        var currentUser = await UserManager.GetUserAsync(User);
        if (currentUser != null)
        {
            if (currentUser!.Name == author)
            {
                IList<string> following = currentUser.Following;
                following.Add(author);
                Cheeps = _cheepService.GetCheepsFromAuthors(following, page);
                following.Remove(author);
            }
            else
            {
                Cheeps = _cheepService.GetCheepsFromAuthor(author, out bool hasNext, page);
            }
        }            
        else
        {
            Cheeps = _cheepService.GetCheepsFromAuthor(author, out bool hasNext, page);
        }

        // Used for page links
        PageNumber = page;
        
        // Used to show/hide next-page button
        HasMorePages = hasNext;
        
        return Page();
    }
}
