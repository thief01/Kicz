namespace KichBackendApp.Models.DTOs;

public class UpdateProfileDto
{
    public string? DisplayName { get; set; } = String.Empty;
    public string? AvatarUrl { get; set; } = String.Empty;
}