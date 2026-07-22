namespace KichBackendApp.Models.DTOs.Profile;

public record UserProfileDto
{
    public string Email { get; set; } = String.Empty;
    public string DisplayName { get; set; } = String.Empty;
    public string? AvatarUrl { get; set; } = String.Empty;
}