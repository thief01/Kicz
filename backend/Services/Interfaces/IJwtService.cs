using KichBackendApp.Models;

namespace KichBackendApp.Services.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}