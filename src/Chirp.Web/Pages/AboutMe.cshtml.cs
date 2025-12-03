using Chirp.Infrastructure.Entities;
using Chirp.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO.Compression;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Chirp.Web.Pages;

public class AboutMe : PageModel
{
    private readonly ICheepService _cheepService;
    private readonly IAuthorService _authorService;
    private readonly UserManager<Author> _userManager;
    private readonly SignInManager<Author> _signInManager;
    public required string DisplayName { get; set; }
    public required string? Email { get; set; }
    public List<CheepViewModel>? Cheeps { get; set; }
    public List<Author> Following { get; set; }

    public AboutMe(ICheepService cheepService, IAuthorService authorService, UserManager<Author> userManager, SignInManager<Author> signInManager)
    {
        _cheepService = cheepService;
        _authorService = authorService;
        _userManager = userManager;
        _signInManager = signInManager;
        Cheeps = new List<CheepViewModel>();
        Following = new List<Author>();
    }
    
    // Handle GET requests
    public async Task OnGet()
    {
        if (User.Identity!.IsAuthenticated) //if the user is logged in, it will show the information about them that is stored in the application 
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                throw new NullReferenceException();
            }

            DisplayName = currentUser.Name;
            Email = currentUser.Email;
            Cheeps = _cheepService.GetCheepsFromAuthor(DisplayName, out bool hasNext, 1); //todo: for now it is just default set to page 1. Either check that it works or let is show all cheeps
            
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

    public void DownloadZip()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        // Delete User and their cheeps
        var author = await _userManager.GetUserAsync(HttpContext.User);
        await _authorService.DeleteAuthor(author.Name);
        
        // Sign out identity
        await _signInManager.SignOutAsync();

        // Sign out OAuth
        await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme); // "Identity.Application"
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme); // "Identity.External"
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); // default cookie

        return LocalRedirect("/");
    }
}