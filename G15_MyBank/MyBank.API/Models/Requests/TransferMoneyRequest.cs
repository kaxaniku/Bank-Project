namespace MyBank.API.Models.Requests;

public sealed record TransferMoneyRequest
{
    public string FromAccountNum { get; set; } = null!;
    public string ToAccountNum { get; set; } = null!;
    public decimal Amount { get; set; }
}
