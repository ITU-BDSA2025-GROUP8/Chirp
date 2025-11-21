using Chirp.Infrastructure.Entities;
using Chirp.Web.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Chirp.Web.Services;
using Microsoft.AspNetCore.Identity;

namespace Chirp.Web.Pages;

/*
 * Each user should have 2 timelines: A public and a private
 * The public contains all cheeps written by the user.
 * This is the timeline visible to other users.
 * 
 * The private contains all cheeps written by the user,
 * and all cheeps written by the authors that the user follows.
 * This timeline is only visible to the authenticated user.
 */

//Pages for cheeps from a specific author
public class UserTimelineModel : TimelineBaseModel
{
    [BindProperty]
    public string? UserName { get; set; }
    
    //Inherits from parent class TimelineBaseModel, which injects the cheep service and sets a model
    public UserTimelineModel(ICheepService service, UserManager<Author> userManager) : base(service, userManager)
    {
    }

    //Gets all cheeps from a specific author
    public async Task<ActionResult> OnGet(string author, [FromQuery] int page, [FromQuery] string? error)
    {
          HandleError(error);
        
        //Call base method to get user info
        await GetUserInformation();   
        
        Cheeps = _service.GetCheepsFromAuthor(author, page);
        return Page();
    }
}
