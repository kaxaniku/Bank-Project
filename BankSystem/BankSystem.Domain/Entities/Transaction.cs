using BankSystem.Domain.Common;
using BankSystem.Domain.Enums;

namespace BankSystem.Domain.Entities;

public class Transaction : BaseEntity
{
    public int? SourceAccountId { get; set; }
    public int? DestinationAccountId { get; set; }
    public decimal Amount { get; set; }
    public decimal? SourceBalanceAfter { get; set; }
    public decimal? DestinationBalanceAfter { get; set; }
    public decimal FeeAmount { get; set; } = 0m;
    public string? FeeCurrency { get; set; }
    public string Currency { get; set; } = null!;
    public string Reference { get; set; } = null!;
    public string Description { get; set; } = null!;
    public TransactionStatus Status { get; set; }
    public TransactionType Type { get; set; }

    public virtual Account? SourceAccount { get; set; }
    public virtual Account? DestinationAccount { get; set; }
}
