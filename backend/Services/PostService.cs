using System.Security.Claims;
using KichBackendApp.Data;
using KichBackendApp.Models;
using KichBackendApp.Models.DTOs.Post;
using KichBackendApp.Models.Exceptions;
using KichBackendApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace KichBackendApp.Services;

public class PostService : IPostService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    public PostService(ApplicationDbContext context,  UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public async Task<PostDto> CreatePost(CreatePostDto createPostDto, string userId)
    {
        var post = new Post(createPostDto, userId);
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return new PostDto(post);
    }

    public async Task<IEnumerable<PostDto>> GetMyPosts(string userId)
    {
        var posts = await _context.Posts
            .Where(p => p.UserId == userId)
            .Include(p => p.User)
            .OrderByDescending(p => p.CreatedAt).ToListAsync();
        return posts.Select(post => new PostDto(post));
    }

    public async Task<IEnumerable<PostDto>> GetFeed()
    {
        var pots = await _context.Posts
            .Include(p => p.User)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
        return  pots.Select(po => new PostDto(po));
    }

    public async Task<PostDto> GetPost(int id, string userId)
    {
        var post = await GetPostData(id, userId);
        return new PostDto(post);
    }

    public async Task<PostDto> UpdatePost(int id, UpdatePostDto updatePostDto, string userId)
    {
        var post = await GetPostData(id, userId);
        post.UpdateFromDto(updatePostDto);
        _context.Posts.Update(post);
        await _context.SaveChangesAsync();
        return new PostDto(post);
    }
    
    public async Task DeletePost(int id, string userId)
    {
        var post = await GetPostData(id, userId);
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
    }

    private async Task<Post> GetPostData(int id, string userId)
    {
        var post = await _context.Posts.Where(p => p.UserId == userId)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (post == null)
        {
            throw new NotFoundException("Post not found");
        }

        return post;
    }
}