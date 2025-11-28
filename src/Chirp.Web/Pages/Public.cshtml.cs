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
    
    // Used for page links
    public int PageNumber { get; set; }
    public bool HasMorePages { get; set; }

    //Get all cheeps by all authors
    public async Task<ActionResult> OnGetAsync([FromQuery] int page = 1, [FromQuery] string? error = null)
    {
        HandleError(error);
        
        //Call base method to get user info
        await GetUserInformation();
        
        Cheeps = Service.GetCheeps(out bool hasNext, page);
        
        // Used to show/hide next-page button
        HasMorePages = hasNext;

        // Used for page links
        PageNumber = page;
        
        return Page();
    }
    
}
