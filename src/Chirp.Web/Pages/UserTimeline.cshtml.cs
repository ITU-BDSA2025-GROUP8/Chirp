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
    public string? UserName { get; set; } //todo: Do we need this?
    
    //Inherits from parent class TimelineBaseModel, which injects the cheep service and sets a model
    public UserTimelineModel(ICheepService service, UserManager<Author> userManager) : base(service, userManager)
    {
    }

    //Gets all cheeps from a specific author
    public async Task<ActionResult> OnGet(string author, [FromQuery] int page)
    {
        //Call base method to get user info
        await GetUserInformation();   
        
        Cheeps = _service.GetCheepsFromAuthor(author, page);
        return Page();
    }
}
