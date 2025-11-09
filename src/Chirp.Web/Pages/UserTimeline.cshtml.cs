using Chirp.Web.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Chirp.Web.Services;

namespace Chirp.Web.Pages;

//Pages for cheeps from a specific author
public class UserTimelineModel : TimelineBaseModel
{
    [BindProperty]
    public string? UserName { get; set; } //todo: Do we need this?
    
    //Inherits from parent class TimelineBaseModel, which injects the cheep service and sets a model
    public UserTimelineModel(ICheepService service) : base(service)
    {
    }

    //Gets all cheeps from a specific author
    public ActionResult OnGet(string author, [FromQuery] int page)
    {
        Cheeps = _service.GetCheepsFromAuthor(author, page);
        return Page();
    }
}
