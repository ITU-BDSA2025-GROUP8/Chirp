using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages;

public class AboutMe : PageModel
{
    public void OnGet()
    {
        Console.WriteLine("About Me");
    }
}