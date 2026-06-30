namespace DTOs.User;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    //public DateTime CreatedAt { get; set; } = DateTime.Now;
}

