using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KichBackendApp.Models;
using KichBackendApp.Models.DTOs;
using KichBackendApp.Models.DTOs.Auth;
using KichBackendApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace KichBackendApp.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User>  _userManager;
    private readonly IConfiguration _configuration;
    private readonly IAuthService  _authService;
    private readonly IJwtService _jwtService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var response = await _authService.RegisterAsync(dto);
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var response = await _authService.LoginAsync(dto);
        return Ok(response);
    }

    [Authorize]
    [HttpGet("verify")]
    public IActionResult Verify()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userName = User.Identity?.Name;

        return Ok(new { 
            isAuthenticated = true,
            userId = userId,
            userName = userName 
        });
    }
}