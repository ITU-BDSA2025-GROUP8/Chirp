using Chirp.Infrastructure.Entities;
using Chirp.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    public IList<string> Following { get; set; }

    public AboutMe(ICheepService cheepService, IAuthorService authorService, UserManager<Author> userManager, SignInManager<Author> signInManager)
    {
        _cheepService = cheepService;
        _authorService = authorService;
        _userManager = userManager;
        _signInManager = signInManager;
        Cheeps = new List<CheepViewModel>();
        Following = new List<String>();
    }
    
    // Handle GET requests
    public async Task<IActionResult> OnGet()
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
            Cheeps = _cheepService.GetCheepsFromAuthorOnOnePage(DisplayName);
            Following = currentUser.Following;
            
        }
        else
        {
            // Redirect to the login page if not authenticated
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        // Delete User and their cheeps
        var author = await _userManager.GetUserAsync(HttpContext.User);
        await _authorService.DeleteAuthor(author!.Name);
        
        // Sign out identity
        await _signInManager.SignOutAsync();

        // Sign out OAuth
        await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme); // "Identity.Application"
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme); // "Identity.External"
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); // default cookie

        return LocalRedirect("/");
    }
}