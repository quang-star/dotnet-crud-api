using DTOs.User;
using Models;
using Repositories.Interfaces;
using Services.Interfaces;
using BCrypt.Net;
using FluentValidation;

namespace Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<CreateUserDto> _createUserValidator;
    private readonly IValidator<UpdateUserDto> _updateUserValidator;

    public UserService(IUserRepository userRepository, IValidator<CreateUserDto> createUserValidator, IValidator<UpdateUserDto> updateUserValidator)
    {
        _userRepository = userRepository;
        _createUserValidator = createUserValidator;
        _updateUserValidator = updateUserValidator;
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        if (users == null || !users.Any())
        {
            throw new Exception("No users found.");
        }
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
            throw new Exception($"User not found");
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
        var validationResult = await _createUserValidator.ValidateAsync(userDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
        var user = new User
        {
            Name = userDto.Name,
            Email = userDto.Email,
            Password = hashedPassword,
            //CreatedAt = DateTime.Now
        };

        var createdUser = await _userRepository.AddAsync(user);
        if (createdUser == null)
        {
            throw new Exception("Failed to create user.");
        }
        return new CreateUserDto
        {
            Name = createdUser.Name,
            Email = createdUser.Email,
            Password = createdUser.Password
        };

    }

    public async Task UpdateUserAsync(int id, UpdateUserDto userDto)
    {

        var validationResult = await _updateUserValidator.ValidateAsync(userDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new Exception($"User not found");
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