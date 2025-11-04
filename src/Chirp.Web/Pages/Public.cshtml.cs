using Chirp.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Web.Services;

namespace Chirp.Web.Pages;

//Pages for cheeps from all authors
public class PublicModel : PageModel
{
    private readonly ICheepService _service;
    public List<CheepViewModel> Cheeps { get; set; }
    [BindProperty]
    public string cheepText { get; set; }

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

    public ActionResult OnPost()
    {
       //Create CheepDTO // todo: get the correct values, this is just dummy stuff
       var cheepDTO = new CheepDTO()
       {
           CreatedAt = DateTime.Now,
           Id = 1,
           Text = cheepText,
           UserName = "TestyTester" //Riders ide: HttpContext.User.Identity.Name
       };
       //Call repository method for creating a cheep
       _service.CreateCheepFromDTO(cheepDTO);
       return RedirectToPage();
    }
}
