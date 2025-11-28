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
    public UserTimelineModel(ICheepService service, UserManager<Author> userManager) : base(service, userManager)
    {
    }
    
    // Used for page links
    public int PageNumber { get; set; }
    public bool HasMorePages { get; set; }

    //Gets all cheeps from a specific author
    public async Task<ActionResult> OnGet(string author, [FromQuery] int page, [FromQuery] string? error)
    {
        HandleError(error);
        
        //Call base method to get user info
        await GetUserInformation();   
        
        // Used for page links
        PageNumber = page;
        
        Cheeps = Service.GetCheepsFromAuthor(author, out bool hasNext, page);
        
        // Used to show/hide next-page button
        HasMorePages = hasNext;
        
        return Page();
    }
}
