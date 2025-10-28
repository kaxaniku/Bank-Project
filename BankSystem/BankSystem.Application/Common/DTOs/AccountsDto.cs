namespace BankSystem.Application.Common.DTOs;

public class AccountsDto
{
    public int Id { get; set; }
    public string AccountNumber { get; set; } = null!;
    public string Currency { get; set; } = null!;
    public decimal Balance { get; set; }
    public string Type { get; set; } = null!;
    public string Status { get; set; } = null!;
}
