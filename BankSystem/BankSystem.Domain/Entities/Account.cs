using BankSystem.Domain.Common;
using BankSystem.Domain.Enums;

namespace BankSystem.Domain.Entities;

public class Account : BaseEntity
{
    public int CustomerId { get; set; }
    public string AccountNumber { get; set; } = null!;
    public string Currency { get; set; } = null!;
    public decimal Balance { get; set; }
    public AccountType Type { get; set; }
    public AccountStatus Status { get; set; }

    public Customer Customer { get; set; } = null!;
    public ICollection<Card> Cards { get; set; } = [];
}
