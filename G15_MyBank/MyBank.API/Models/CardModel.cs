namespace MyBank.API.Models;

public sealed record CardModel
{
    public string CardNumber { get; set; } = null!;
    public byte CardType { get; set; }
    public byte Status { get; set; }
    public string CVC { get; set; } = null!;
    public DateTime ExpirationDate { get; set; }
}