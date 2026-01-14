using System.Globalization;
using System.Security.Claims;
using KichBackendApp.Models;
using KichBackendApp.Models.DTOs;
using KichBackendApp.Models.DTOs.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KichBackendApp.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    public UserController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Unauthorized();
        
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return NotFound();

        return Ok(new UserProfileDto()
        {
            Email = user.Email,
            DisplayName = user.DisplayName,
            AvatarUrl = user.AvatarUrl,
        });
    }

    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UserProfileDto dto)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Unauthorized();

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return NotFound();

        if (!string.IsNullOrEmpty(dto.DisplayName))
            user.DisplayName = dto.DisplayName;

        if (!string.IsNullOrEmpty(dto.AvatarUrl))
            user.AvatarUrl = dto.AvatarUrl;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(new UserProfileDto()
        {
            Email = user.Email,
            DisplayName = user.DisplayName,
            AvatarUrl = user.AvatarUrl,
        });
    }
    [HttpPost("avatar")]
    public async Task<IActionResult> UploadAvatar(IFormFile file)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    
        if (userId == null)
            return Unauthorized();
    
        // Walidacja
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded");
    
        // Tylko obrazy
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
    
        if (!allowedExtensions.Contains(extension))
            return BadRequest("Invalid file type. Only images allowed.");
    
        // Max 5MB
        if (file.Length > 5 * 1024 * 1024)
            return BadRequest("File too large. Max 5MB.");
    
        // Ścieżka do zapisu
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "avatars");
        Directory.CreateDirectory(uploadsFolder); // Utwórz folder jeśli nie istnieje
    
        var fileName = $"{userId}{extension}";
        var filePath = Path.Combine(uploadsFolder, fileName);
    
        // Usuń stary avatar jeśli istnieje
        if (System.IO.File.Exists(filePath))
            System.IO.File.Delete(filePath);
    
        // Zapisz nowy
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
    
        // Update user w bazie
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound();
    
        user.AvatarUrl = $"/uploads/avatars/{fileName}";
        await _userManager.UpdateAsync(user);
    
        return Ok(new { avatarUrl = user.AvatarUrl });
    }
}