using System.ComponentModel.DataAnnotations;

namespace DTOs.User;

public class CreateUserDto
{
    
    public string Name {get; set; } = string.Empty;

    public string Email { get; set; } = String.Empty;

    public string Password { get; set; }  = string.Empty;
}

