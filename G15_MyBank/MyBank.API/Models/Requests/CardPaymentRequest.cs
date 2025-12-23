namespace MyBank.API.Models.Requests;

public sealed record CardPaymentRequest
{
    public string CardNum { get; set; } = null!;
    public string ReceiverAccountNum { get; set; } = null!;
    public decimal Amount { get; set; }
}