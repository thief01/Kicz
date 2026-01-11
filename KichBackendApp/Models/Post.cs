using KichBackendApp.Models.DTOs.Post;

namespace KichBackendApp.Models;

public class Post
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ScheduledFor { get; set; }
    public bool IsPublished { get; set; } = false;

    public string UserId { get; set; } = string.Empty;
    public User User { get; set; } = null!;

    public Post()
    {
        
    }

    public Post(CreatePostDto createPostDto)
    {
        Content = createPostDto.Content;
        ImageUrl = createPostDto.ImageUrl;
        ScheduledFor = createPostDto.ScheduledFor;
    }

    public void UpdateFromDto(UpdatePostDto dto)
    {
        if (dto.Content != null)
            Content = dto.Content;
        
        if(dto.ImageUrl != null)
            ImageUrl = dto.ImageUrl;
        
        if(dto.ScheduledFor.HasValue)
            ScheduledFor = dto.ScheduledFor;
        
        if(dto.IsPublished.HasValue)
            IsPublished = dto.IsPublished.Value;
    }
}