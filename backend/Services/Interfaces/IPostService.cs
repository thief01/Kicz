using KichBackendApp.Models.DTOs.Post;

namespace KichBackendApp.Services.Interfaces;

public interface IPostService
{
    public Task<PostDto> CreatePost(CreatePostDto createPostDto, string userId);
    public Task<IEnumerable<PostDto>> GetMyPosts(string userId);
    public Task<IEnumerable<PostDto>> GetFeed();
    public Task<PostDto> GetPost(int id, string userId);
    public Task<PostDto> UpdatePost(int id, UpdatePostDto updatePostDto, string userId);
    public Task DeletePost(int id, string userId);
    
}