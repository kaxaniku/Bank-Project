namespace BankSystem.Application.Common.DTOs.User;

public class RegistrationRequestDto
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
