using Microsoft.AspNetCore.Identity;

namespace Chirp.Infrastructure.Data;

public class ApplicationUser : IdentityUser
{
    public string? DisplayName { get; set; }
}
