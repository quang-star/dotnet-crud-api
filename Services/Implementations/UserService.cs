using DTOs.User;
using Models;
using Repositories.Interfaces;
using Services.Interfaces;
using BCrypt.Net;

namespace Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(user => new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            //CreatedAt = user.CreatedAt
        }).ToList();
    }

    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return null;
        }

        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            //CreatedAt = user.CreatedAt
        };
    }

    public async Task<CreateUserDto?> CreateUserAsync(CreateUserDto userDto)
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
        var user = new User
        {
            Name = userDto.Name,
            Email = userDto.Email,
            Password = hashedPassword,
            //CreatedAt = DateTime.Now
        };

        var createdUser = await _userRepository.AddAsync(user);

        return new CreateUserDto
        {
            Name = createdUser.Name,
            Email = createdUser.Email,
            Password = createdUser.Password
        };
        
    }

    public async Task UpdateUserAsync(int id, UpdateUserDto userDto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        if (!string.IsNullOrEmpty(userDto.Name))
        {
            user.Name = userDto.Name;
        }
        if (!string.IsNullOrEmpty(userDto.Email))
        {
            user.Email = userDto.Email;
        }   
        if (!string.IsNullOrEmpty(userDto.Password))
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            user.Password = hashedPassword;
        }

        await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        await _userRepository.DeleteAsync(user);
    }

    
}