using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBank.Domain;

public enum TransactionStatus : byte
{
    Pending = 0,
    Completed = 1,
    Failed = 2,
    Blocked = 3
}

public sealed class Transaction
{
    [Key]
    public int TransactionId { get; set; }

    public DateTime TransactionDate { get; set; }

    [Column(TypeName = "MONEY")]
    public decimal Amount { get; set; }

    [MaxLength(250)]
    public string? Description { get; set; }

    public TransactionStatus Status { get; set; }

    public int FromAccountId { get; set; }
    public int ToAccountId { get; set; }

    public Account FromAccount { get; set; } = null!;
    public Account ToAccount { get; set; } = null!;
}