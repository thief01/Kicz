using KichBackendApp.Models.DTOs.Comment;
using KichBackendApp.Models.DTOs.Post;

namespace KichBackendApp.Services.Interfaces;

public interface ICommentService
{
    Task<CommentDto> AddComment(CreateCommentDto createCommentDto);

    Task<CommentDto> GetComment(int id);
}