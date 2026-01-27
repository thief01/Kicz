using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using KichBackendApp.Data;
using KichBackendApp.Models;
using KichBackendApp.Models.DTOs.Post;
using KichBackendApp.Models.Exceptions;
using KichBackendApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KichBackendApp.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PostController : ControllerBase
{
    private readonly IPostService  _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyPosts()
    {
        var userId = GetUserId();
        var posts = await _postService.GetMyPosts(userId);
        return Ok(posts);
    }

    [AllowAnonymous]
    [HttpGet("feed")]
    public async Task<IActionResult> GetFeed()
    {
        var posts = await _postService.GetFeed();
        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPost(int id)
    {
        var userId = GetUserId();
        var post = await _postService.GetPost(id, userId);
        return Ok(post);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDto postDto)
    {
        var userId = GetUserId();
        var post = await _postService.CreatePost(postDto, userId);
        return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(int id, [FromBody] UpdatePostDto postDto)
    {
        var userId = GetUserId();
        var post = await _postService.UpdatePost(id, postDto, userId);
        return Ok(post);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var userId = GetUserId();
        await _postService.DeletePost(id, userId);
        return NoContent();
    }

    private string GetUserId()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            throw new UnauthorizedException("User not logged in");
        return userId;
    }
}