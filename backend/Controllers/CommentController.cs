using KichBackendApp.Data;
using KichBackendApp.Models;
using KichBackendApp.Models.DTOs.Comment;
using KichBackendApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KichBackendApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly ApplicationDbContext _context;
    private readonly ICommentService  _commentService;

    public CommentController(UserManager<User> userManager, ApplicationDbContext context,  ICommentService commentService)
    {
        _userManager = userManager;
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCommentDto createCommentDto)
    {
        var post = _context.Posts.First(post => post.Id == createCommentDto.PostId);
        if (post == null)
        {
            return NotFound();
        }

        var comment = new Comment(createCommentDto);
        post.Comments.Add(comment);
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
        return Ok(comment);
    }
}