using BankSystem.Domain.Common;
using BankSystem.Domain.Enums;

namespace BankSystem.Domain.Entities;

public class Card : BaseEntity
{
    public int AccountId { get; set; }
    public string CardNumber { get; set; } = null!;
    public string CardHolderName { get; set; } = null!;
    public DateTime ExpirationDate { get; set; }
    public string CVV { get; set; } = null!;
    public CardType Type { get; set; }
    public CardStatus Status { get; set; }

    public Account Account { get; set; } = null!;
}
