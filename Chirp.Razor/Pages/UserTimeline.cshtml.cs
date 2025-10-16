using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Razor.Pages;

//Pages for cheeps from a specific author
public class UserTimelineModel : PageModel
{
    private readonly ICheepService _service;
    public List<CheepViewModel> Cheeps { get; set; }

    //Inject the cheep service, sets a specific "model"
    public UserTimelineModel(ICheepService service)
    {
        _service = service;
    }

    //Gets all cheeps from a specific author
    public ActionResult OnGet(string author, [FromQuery] int page)
    {
        Cheeps = _service.GetCheepsFromAuthor(author, page);
        return Page();
    }
}
