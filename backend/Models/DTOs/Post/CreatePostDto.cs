namespace KichBackendApp.Models.DTOs.Post;

public record CreatePostDto
{
    public string Content { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public DateTime? ScheduledFor { get; set; }
}