using BankSystem.Application.Common.DTOs.Card;

namespace BankSystem.Application.Common.DTOs.Account;

public class GetAccountResponseDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = null!;
    public string AccountNumber { get; set; } = null!;
    public string Currency { get; set; } = null!;
    public decimal Balance { get; set; }
    public string Type { get; set; } = null!;
    public string Status { get; set; } = null!;

    public List<CardDto>? Cards { get; set; }
}
