using Chirp.Core.DTO;
using Chirp.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages.Shared;

public class TimelineBaseModel : PageModel
{
    private readonly ICheepService _service;
    public List<CheepViewModel>? Cheeps { get; set; }
    [BindProperty]
    public string? CheepText { get; set; }
    [BindProperty]
    public string? UserId { get; set; }
    
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