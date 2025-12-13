namespace MyBank.API.Models;

public sealed record AccountModel
{
    public string AccountNumber { get; set; } = null!;
    public int CustomerId { get; set; }
    public decimal Balance { get; set; }
    public byte Status { get; set; }
}