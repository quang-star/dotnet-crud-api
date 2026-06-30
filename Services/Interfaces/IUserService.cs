using DTOs.User;
using Models;

namespace Services.Interfaces;

public interface IUserService
{
    Task<List<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<CreateUserDto?> CreateUserAsync(CreateUserDto userDto);
    Task UpdateUserAsync(int id, UpdateUserDto userDto);
    Task DeleteUserAsync(int id);
}