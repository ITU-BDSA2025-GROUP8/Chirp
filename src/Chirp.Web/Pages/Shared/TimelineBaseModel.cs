using Chirp.Core.DTO;
using Chirp.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages.Shared;

public class TimelineBaseModel : PageModel
{
    protected readonly ICheepService _service; //set to protected to be accessible in child classes //todo: Er der en grund til, at den hedder _service og ikke Service?
    public List<CheepViewModel>? Cheeps { get; set; }
    [BindProperty]
    public string? CheepText { get; set; }
    [BindProperty]
    public string? UserId { get; set; }
    
    //Inject the cheep service, sets a specific "model"
    public TimelineBaseModel(ICheepService service)
    {
        _service = service;
        Cheeps = new List<CheepViewModel>();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        //Create CheepDTO
        var cheepDTO = new CheepDTO()
        {
            CreatedAt = DateTime.Now,
            Text = CheepText,
            AuthorId = UserId
        };
       
        //Call the repository method for creating a cheep
        await _service.CreateCheepFromDTO(cheepDTO);
        return RedirectToPage();
    }
}