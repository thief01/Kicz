namespace KichBackendApp.Models.DTOs.Comment;

public record CreateCommentDto
{
    public int PostId { get; set; }
    public string Author { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime? PublishDate { get; set; }
}