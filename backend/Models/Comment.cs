using KichBackendApp.Models.DTOs.Comment;

namespace KichBackendApp.Models;

public class Comment
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public string Content { get; set; }
    public string AuthorName { get; set; }
    public DateTime? CreatedAt { get; set; }

    public Comment()
    {
        
    }

    public Comment(CreateCommentDto createCommentDto)
    {
        PostId  = createCommentDto.PostId;
        Content = createCommentDto.Content;
        AuthorName = createCommentDto.Author;
        CreatedAt = createCommentDto.PublishDate;
    }
}