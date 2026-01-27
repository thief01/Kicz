namespace KichBackendApp.Models.DTOs.Comment;

public class CreateCommentDto
{
    public int PostId { get; set; }
    public string Author { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime? PublishDate { get; set; }
}