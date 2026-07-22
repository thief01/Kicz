namespace KichBackendApp.Models.DTOs.Profile;

public record UpdateProfileDto
{
    public string? DisplayName { get; set; } = String.Empty;
    public string? AvatarUrl { get; set; } = String.Empty;
}