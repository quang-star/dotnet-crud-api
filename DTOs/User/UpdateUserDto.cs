using System.ComponentModel.DataAnnotations;


namespace DTOs.User;

public class UpdateUserDto
{
    [StringLength(50, MinimumLength = 3)]
    public string? Name { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [MinLength(6)]
    public string? Password { get; set; }
}