using Models;

namespace Services.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}