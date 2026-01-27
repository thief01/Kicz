namespace KichBackendApp.Models.DTOs.Comment;

public class CommentDto
{
    public string Author { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; set; }

    public CommentDto(Models.Comment comment)
    {
        Author = comment.AuthorName;
        Content = comment.Content;
        CreatedAt = comment.CreatedAt;
    }
}