using Chirp.Infrastructure.Entities;
using Chirp.Web.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Chirp.Web.Services;
using Microsoft.AspNetCore.Identity;

namespace Chirp.Web.Pages;

//Pages for cheeps from all authors
public class PublicModel : TimelineBaseModel
{
    
    //Inherits from parent class TimelineBaseModel, which injects the cheep service and sets a model
    public PublicModel(ICheepService service, UserManager<Author> userManager) : base(service, userManager)
    {
    }

    //Get all cheeps by all authors
    public async Task<ActionResult> OnGetAsync([FromQuery] int page, [FromQuery] string? error)
    {
        HandleError(error);
        
        //Call base method to get user info
        await GetUserInformation();
        
        Cheeps = _service.GetCheeps(page);
        return Page();
    }
    
}
