using Chirp.Infrastructure.Entities;
using Chirp.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages;

//todo: rename Feature and Information in table in cshtml file

public class AboutMe : PageModel
{
    private readonly ICheepService Service;
    private readonly UserManager<Author> UserManager;
    public required string DisplayName { get; set; }
    public required string? Email { get; set; }
    public List<CheepViewModel>? Cheeps { get; set; }
    public List<Author> Following { get; set; }
    //todo: check up on other possible properties

    public AboutMe(ICheepService service, UserManager<Author> userManager)
    {
        Service = service;
        UserManager = userManager;
        Cheeps = new List<CheepViewModel>();
        Following = new List<Author>();
    }
    
    // Handle GET requests
    public async Task OnGet()
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
            Cheeps = Service.GetCheepsFromAuthor(DisplayName, 1); //todo: for now it is just default set to page 1
            
        }
        else
        {
            // Redirect to the login page if not authenticated
            Response.Redirect($"{Request.Path}?error=not_authenticated"); //todo: temporary, should Redirect to login page with an error
            return;
        }
        
        
        //todo: tests - remove when done
        /*
         * If user is authenticated/logged in:
         * - Show user's name and email
         * - Show user's cheeps
         * - Show user's following'
         * If user is not authenticated/logged in:
         * - Redirect to login page
         */
    }
}