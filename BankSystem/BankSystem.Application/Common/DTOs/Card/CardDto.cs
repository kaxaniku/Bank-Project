namespace BankSystem.Application.Common.DTOs.Card;

public class CardDto
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public string CardNumber { get; set; } = null!;
    public string CardHolderName { get; set; } = null!;
    public DateTime ExpirationDate { get; set; }
    public string Type { get; set; } = null!;
    public string Status { get; set; } = null!;
}
