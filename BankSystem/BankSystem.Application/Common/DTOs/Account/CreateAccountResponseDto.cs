namespace BankSystem.Application.Common.DTOs.Account;

public class CreateAccountResponseDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string AccountNumber { get; set; } = null!;
    public string Currency { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string Status { get; set; } = null!;
}