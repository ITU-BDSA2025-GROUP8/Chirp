using Chirp.Core.DTO;
using Chirp.Infrastructure.Entities;
using Chirp.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages.Shared;

public class TimelineBaseModel : PageModel
{
    protected readonly ICheepService _service; //set to protected to be accessible in child classes //todo: Er der en grund til, at den hedder _service og ikke Service?
    public List<CheepViewModel>? Cheeps { get; set; }
    [BindProperty]
    public string? CheepText { get; set; }
    [BindProperty]
    public string? UserId { get; set; }
    public string? DisplayName { get; set; }
    protected readonly UserManager<Author> UserManager;
    
    //Inject the cheep service, sets a specific "model"
    public TimelineBaseModel(ICheepService service, UserManager<Author> userManager)
    {
        _service = service;
        UserManager = userManager;
        Cheeps = new List<CheepViewModel>();
    }

    //Get current user information
    //Obs: Used ChatGPT to help figure out how to get userManager info from public.cshtml (html) to this class (c#)
    public async Task GetUserInformation()
    {
        if (User.Identity!.IsAuthenticated)
        {
            var currentUser = await UserManager.GetUserAsync(User);
            DisplayName = currentUser?.Name ?? User.Identity.Name;
            UserId = currentUser.Id; 
        }
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        //Create CheepDTO
        var cheepDTO = new CheepDTO()
        {
            CreatedAt = DateTime.Now,
            Text = CheepText,
            AuthorId = UserId
        };
       
        //Call the repository method for creating a cheep
        await _service.CreateCheepFromDTO(cheepDTO);
        return RedirectToPage();
    }
}