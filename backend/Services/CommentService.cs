using KichBackendApp.Data;
using KichBackendApp.Models;
using KichBackendApp.Models.DTOs.Comment;
using KichBackendApp.Models.DTOs.Post;
using KichBackendApp.Models.Exceptions;
using KichBackendApp.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KichBackendApp.Services;

public class CommentService : ICommentService
{
    private readonly UserManager<User> _userManager;
    private readonly ApplicationDbContext _context;

    public CommentService(UserManager<User> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }


    public async Task<CommentDto> AddComment(CreateCommentDto createCommentDto)
    {
        var post  = await _context.Posts.FirstOrDefaultAsync(p => p.Id == createCommentDto.PostId);
        if (post == null)
        {
            throw new NotFoundException("Post not found");
        }

        var comment = new Comment(createCommentDto);
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
        return new CommentDto(comment);
    }

    public async Task<CommentDto> GetComment(int id)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        if (comment == null)
        {
            throw new NotFoundException("Comment not found");
        }
        return new CommentDto(comment);
    }
}