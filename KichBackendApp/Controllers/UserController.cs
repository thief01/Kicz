using System.Globalization;
using KichBackendApp.Models;
using KichBackendApp.Models.DTOs;
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
}