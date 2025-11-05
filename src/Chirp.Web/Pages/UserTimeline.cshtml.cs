using Chirp.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Web.Services;

namespace Chirp.Web.Pages;

//Pages for cheeps from a specific author
public class UserTimelineModel : PageModel
{
    private readonly ICheepService _service;
    public List<CheepViewModel>? Cheeps { get; set; }
    [BindProperty]
    public string? CheepText { get; set; }

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
    
    public async Task<IActionResult> OnPostAsync()
    {
        //Create CheepDTO // todo: get the correct values, this is just dummy stuff
        var cheepDTO = new CheepDTO()
        {
            CreatedAt = DateTime.Now,
            Id = 1,
            Text = CheepText,
            UserName = "TestyTester" //Riders ide: HttpContext.User.Identity.Name 
        };
       
        //Call the repository method for creating a cheep
        await _service.CreateCheepFromDTO(cheepDTO);
        return RedirectToPage();
    }
}
