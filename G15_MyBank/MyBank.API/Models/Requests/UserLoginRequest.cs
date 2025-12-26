namespace MyBank.API.Models.Requests;

public class UserLoginRequest
{
    public string Name { get; set; } = null!;
    public string Password { get; set; } = null!;
}