using DTOs.Auth;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var token = await _authService.LoginAsync(dto);

        return Ok(new
        {
            AccessToken = token
        });
    }
}