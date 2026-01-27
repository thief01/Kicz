using KichBackendApp.Data;
using KichBackendApp.Models;
using KichBackendApp.Models.DTOs.Comment;
using KichBackendApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KichBackendApp.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CommentController : ControllerBase
{
    private readonly ICommentService  _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCommentDto createCommentDto)
    {
        var comment = await _commentService.AddComment(createCommentDto);
        return Ok(comment);
    }

    [HttpGet]
    public async Task<IActionResult> Get(int id)
    {
        var comment = await _commentService.GetComment(id);
        return Ok(comment);
    }
}