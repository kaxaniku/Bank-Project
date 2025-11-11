namespace BankSystem.Application.Common.DTOs.User;

public class LoginRequestDto
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}
