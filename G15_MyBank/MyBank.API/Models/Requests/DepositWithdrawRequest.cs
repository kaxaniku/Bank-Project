namespace MyBank.API.Models.Requests;

public sealed record DepositWithdrawRequest
{
    public string AccountNum { get; set; } = null!;
    public decimal Amount { get; set; }
}
