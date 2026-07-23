namespace KichBackendApp.Models.DTOs.Post;

public record PostDto
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ScheduledFor { get; set; }
    public DateTime ModifiedAt { get; set; }
    public bool IsModified { get; set; }
    public bool ? IsPublished { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string UserDisplayName { get; set; } = string.Empty;

    public PostDto()
    {
        
    }
    
    public PostDto(Models.Post post)
    {
        Id = post.Id;
        Content = post.Content;
        ImageUrl = post.ImageUrl;
        CreatedAt = post.CreatedAt;
        ScheduledFor = post.ScheduledFor;
        IsPublished = post.IsPublished;
        UserId = post.UserId;
        UserDisplayName = post.User?.DisplayName ?? "";
        ModifiedAt = post.ModifiedAt;
        IsModified = post.IsModified;
    }
}