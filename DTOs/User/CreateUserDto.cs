using System.ComponentModel.DataAnnotations;

namespace DTOs.User;

public class CreateUserDto
{
    
    [StringLength(50, MinimumLength = 3)]
    public required string Name {get; set; }

    [EmailAddress]
    public required string Email { get; set; }

    [MinLength(6)]
    public required string Password { get; set; }
}

