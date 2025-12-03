using Chirp.Infrastructure.Entities;
using Chirp.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO.Compression;
using Microsoft.AspNetCore.Mvc;

namespace Chirp.Web.Pages;

public class AboutMe : PageModel
{
    private readonly ICheepService Service;
    private readonly UserManager<Author> UserManager;
    public required string DisplayName { get; set; }
    public required string? Email { get; set; }
    public List<CheepViewModel>? Cheeps { get; set; }
    public IList<string> Following { get; set; }

    public AboutMe(ICheepService service, UserManager<Author> userManager)
    {
        Service = service;
        UserManager = userManager;
        Cheeps = new List<CheepViewModel>();
        Following = new List<String>();
    }
    
    // Handle GET requests
    public async Task<IActionResult> OnGet()
    {
        if (User.Identity!.IsAuthenticated) //if the user is logged in, it will show the information about them that is stored in the application 
        {
            var currentUser = await UserManager.GetUserAsync(User);
            if (currentUser == null)
            {
                throw new NullReferenceException();
            }

            DisplayName = currentUser.Name;
            Email = currentUser.Email;
            Cheeps = Service.GetCheepsFromAuthorOnOnePage(DisplayName);
            Following = currentUser.Following;
            
        }
        else
        {
            // Redirect to the login page if not authenticated
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        return Page();
    }
}