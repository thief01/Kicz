namespace KichBackendApp.Models.DTOs.Post;

public record UpdatePostDto
{
    public string? Content { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime? ScheduledFor { get; set; }
    public bool? IsPublished { get; set; }
}