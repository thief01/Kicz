using KichBackendApp.Models.DTOs.Auth;

namespace KichBackendApp.Services.Interfaces;

public interface IAuthService
{
    public Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
    public Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
}