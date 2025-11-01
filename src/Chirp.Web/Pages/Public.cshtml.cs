using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Web.Services;

namespace Chirp.Web.Pages;

//Pages for cheeps from all authors
public class PublicModel : PageModel
{
    private readonly ICheepService _service;
    public List<CheepViewModel>? Cheeps { get; set; }

    //Inject the cheep service, sets a specific "model"
    public PublicModel(ICheepService service)
    {
        _service = service;
    }

    //Get all cheeps by all authors
    public ActionResult OnGet([FromQuery] int page)
    {
        Cheeps = _service.GetCheeps(page);
        return Page();
    }
}
