using DTOs.Auth;
using Repositories.Interfaces;
using Services.Interfaces;
using BCrypt.Net;
namespace Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public AuthService(
        IUserRepository userRepository,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);

        if (user == null)
        {
            throw new Exception("User not found.");
        }

        string hashPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
        {
            throw new Exception("Invalid password.");
        }

        return _jwtService.GenerateToken(user);
    }
}