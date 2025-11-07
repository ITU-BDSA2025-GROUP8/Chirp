using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Web.Services;

namespace Chirp.Web.Pages;

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
