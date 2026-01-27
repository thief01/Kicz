using KichBackendApp.Models;
using KichBackendApp.Models.DTOs.Auth;
using KichBackendApp.Models.Exceptions;
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
    
    public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        var existingUser =  await _userManager.FindByEmailAsync(registerDto.Email);
        if (existingUser != null)
        {
            
            var errors = new Dictionary<string, string[]>
            {
                { "Email", new[] { "This email already exists" } }
            };
            throw new ValidationException("Email already exists", errors);
        }

        var user = new User()
        {
            Email = registerDto.Email,
            UserName = registerDto.Email,
            DisplayName = registerDto.DisplayName,
        };
        
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.GroupBy(e => e.Code)
                .ToDictionary(g => g.Key,
                    g => g.Select(e => e.Description).ToArray());
            _logger.LogWarning("User creation failed: {Email} errors: {Errors}", registerDto.Email, 
                result.Errors.Select(e => e.Description));
            throw new ValidationException("User creation failed", errors);
        }
        _logger.LogInformation("User created successfully: {Email}", user.Email);
        
        var token = _jwtService.GenerateToken(user);
        
        return new AuthResponseDto
        {
            Token = token,
            Email = user.Email!,
            DisplayName = user.DisplayName!,
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user == null)
        {
            _logger.LogWarning("Unsuccessful login - not found user: {Email}", loginDto.Email);
            throw new AuthenticationException("Invalid login or password");
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);

        if (!isPasswordValid)
        {
            _logger.LogWarning("Unsuccessful login - wrong password for {Email}", loginDto.Email);
            throw new AuthenticationException("Invalid login or password");
        }
        
        _logger.LogInformation("Successful login: {Email}", loginDto.Email);

        var token = _jwtService.GenerateToken(user);
        return new AuthResponseDto()
        {
            Token = token,
            Email = user.Email!,
            DisplayName = user.DisplayName!
        };
    }
}