namespace MyBank.API.Models;

public class UserLoginRequest
{
    public string Name { get; set; } = null!;
    public string Password { get; set; } = null!;
}