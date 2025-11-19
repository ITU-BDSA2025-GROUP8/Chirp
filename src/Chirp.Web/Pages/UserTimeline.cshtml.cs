using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Web.Services;

namespace Chirp.Web.Pages;

/*
 * Each user should have 2 timelines: A public and a private
 * The public contains all cheeps written by the user.
 * This is the timeline visible to other users.
 * 
 * The private contains all cheeps written by the user,
 * and all cheeps written by the authors that the user follows.
 * This timeline is only visible to the authenticated user.
 */

//Pages for cheeps from a specific author
public class UserTimelineModel : PageModel
{
    private readonly ICheepService _service;
    public List<CheepViewModel>? Cheeps { get; set; }

    //Inject the cheep service, sets a specific "model"
    public UserTimelineModel(ICheepService service)
    {
        _service = service;
        Cheeps = new List<CheepViewModel>();
    }

    //Gets all cheeps from a specific author
    public ActionResult OnGet(string author, [FromQuery] int page)
    {
        Cheeps = _service.GetCheepsFromAuthor(author, page);
        return Page();
    }
}
