namespace BankSystem.Application.Common.DTOs.Account;

public class CreateAccountRequestDto
{
    public int CustomerId { get; set; }
    public string Type { get; set; } = null!;
    public string Currency { get; set; } = "USD";
}
