using Chirp.Web.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Chirp.Web.Services;

namespace Chirp.Web.Pages;

//Pages for cheeps from all authors
public class PublicModel : TimelineBaseModel
{
    
    //Inherits from parent class TimelineBaseModel, which injects the cheep service and sets a model
    public PublicModel(ICheepService service) : base(service)
    {
    }

    //Get all cheeps by all authors
    public ActionResult OnGet([FromQuery] int page)
    {
        Cheeps = _service.GetCheeps(page);
        return Page();
    }
    
}
