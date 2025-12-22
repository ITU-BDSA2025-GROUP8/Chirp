using Chirp.Core.DTO;
using Chirp.Core.Interfaces;
using Chirp.Infrastructure.Entities;
using Chirp.Web.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace Chirp.Web.Pages;

//Pages for cheeps from all authors
public class PublicModel : TimelineBaseModel
{
    
    //Inherits from parent class TimelineBaseModel, which injects the cheep service and sets a model
    public PublicModel(ICheepService cheepService, IAuthorService authorService, UserManager<Author> userManager) : base(cheepService, authorService, userManager)
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
        
        Cheeps = _cheepService.GetCheeps(out bool hasNext, page);
        
        // Used to show/hide next-page button
        HasMorePages = hasNext;

        // Used for page links
        PageNumber = page;
        
        return Page();
    }

    public async Task<ActionResult> OnPostLikeAsync(int cheep, string returnUrl)
    {
        var currentUser = await UserManager.GetUserAsync(User);
       await _cheepService.LikeCheep(cheep, currentUser!.Name);
       return LocalRedirect(returnUrl);
    }

    public async Task<ActionResult> OnPostUnLikeAsync(int cheep,string returnUrl)
    {
        var currentUser = await UserManager.GetUserAsync(User);
        await _cheepService.UnLikeCheep(cheep, currentUser!.Name);
        return LocalRedirect(returnUrl);
    }
    
}
