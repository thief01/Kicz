using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KichBackendApp.Models;
using KichBackendApp.Models.DTOs;
using KichBackendApp.Models.DTOs.Auth;
using KichBackendApp.Services.Interfaces;
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

    public AuthController(UserManager<User> userManager, IConfiguration configuration, IAuthService authService, IJwtService jwtService)
    {
        _userManager = userManager;
        _configuration = configuration;
        _authService = authService;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var user = new User()
        {
            Email = dto.Email,
            UserName = dto.Email,
            DisplayName = dto.DisplayName,
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(",", result.Errors.Select(e => e.Description));
            return BadRequest(new {message = errors});
        }

        var token = _jwtService.GenerateToken(user);

        return Ok(new AuthResponseDto()
        {
            Token = token,
            Email = user.Email!,
            DisplayName = user.DisplayName!,
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
        {
            return Unauthorized(new {message = "Invalid credentials"});
        }
        var token = _jwtService.GenerateToken(user);
        return Ok(new AuthResponseDto()
        {
            Token = token,
            Email = dto.Email,
            DisplayName = user.DisplayName!
        });
    }
}