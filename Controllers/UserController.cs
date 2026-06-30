using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using DTOs.User;
using Microsoft.AspNetCore.Authorization;

namespace Controllers;

[ApiController]
// [Authorize]
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
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<CreateUserDto?>> CreateUser(CreateUserDto userDto)
    {
        var createdUser = await _userService.CreateUserAsync(userDto);
        if (createdUser == null)
        {
            return BadRequest();
        }
        return Ok(createdUser);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser(int id, UpdateUserDto userDto)
    {
       
       var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        await _userService.UpdateUserAsync(id, userDto);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        await _userService.DeleteUserAsync(id);
        return NoContent();
    }

}
