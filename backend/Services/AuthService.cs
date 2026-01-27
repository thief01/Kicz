using KichBackendApp.Models;
using KichBackendApp.Models.DTOs.Auth;
using KichBackendApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace KichBackendApp.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtService _jwtService;
    private readonly ILogger<AuthService> _logger;

    public AuthService(UserManager<User> userManager, IJwtService jwtService, ILogger<AuthService> logger)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _logger = logger;
    }

    public Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        throw new NotImplementedException();
    }

    public Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        throw new NotImplementedException();
    }
}