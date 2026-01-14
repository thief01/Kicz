namespace KichBackendApp.Models.DTOs.Post;

public class CreatePostDto
{
    public string Content { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public DateTime? ScheduledFor { get; set; }
}