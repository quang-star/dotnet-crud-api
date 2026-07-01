using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using DTOs.User;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;
namespace Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;


    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto?>> GetUserById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser(CreateUserDto userDto)
    {

        var createdUser = await _userService.CreateUserAsync(userDto);
        return Ok(createdUser);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser(int id, UpdateUserDto userDto)
    {
        await _userService.UpdateUserAsync(id, userDto);
        return NoContent();

    }

    /// [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUserAsync(id);
        return NoContent();
    }

}
