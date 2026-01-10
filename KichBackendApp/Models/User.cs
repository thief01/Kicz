using Microsoft.AspNetCore.Identity;

namespace KichBackendApp.Models;

public class User : IdentityUser
{
    public string? DisplayName { get; set; }
    public string? AvatarUrl { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
}