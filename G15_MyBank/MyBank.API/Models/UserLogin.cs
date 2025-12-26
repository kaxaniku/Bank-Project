namespace MyBank.API.Models;

public class UserLogin
{
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
}