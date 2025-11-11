namespace BankSystem.Application.Common.DTOs.Account;

public class BalancePerCurrency
{
    public string Currency { get; set; } = null!;
    public decimal Balance { get; set; }
}
