using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using KichBackendApp.Data;
using KichBackendApp.Models;
using KichBackendApp.Models.DTOs.Post;
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
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User>  _userManager;

    public PostController(ApplicationDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyPosts()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var posts = await _context.Posts
            .Where(p => p.UserId == userId)
            .Include(p => p.User)
            .OrderByDescending(p => p.CreatedAt).ToListAsync();
        return Ok(posts.Select(p => new PostDto(p)));
    }

    [AllowAnonymous]
    [HttpGet("feed")]
    public async Task<IActionResult> GetFeed()
    {
        var posts = await _context.Posts
            .Include(p => p.User)  // Żeby mieć dane użytkownika (userDisplayName)
            .OrderByDescending(p => p.CreatedAt)  // Od najnowszych
            .ToListAsync();
        
        return Ok(posts.Select(p => new PostDto(p)));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPost(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var post = await _context.Posts.Where(p => p.UserId == userId)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (post == null)
            return NotFound();

        return Ok(new PostDto(post));
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDto postDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    
        if (userId == null)
        {
            return Unauthorized();
        }
        
        var post = new Post(postDto, userId);

        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPost), new { id = post.Id }, new PostDto(post));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(int id, [FromBody] UpdatePostDto postDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Unauthorized();
        
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

        if (post == null)
            return NotFound();
        
        post.UpdateFromDto(postDto);
        await _context.SaveChangesAsync();

        var user = await _userManager.FindByIdAsync(userId!);

        return Ok(new PostDto(post));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (userId == null)
            return Unauthorized();
            
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

        if (post == null)
            return NotFound();

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}