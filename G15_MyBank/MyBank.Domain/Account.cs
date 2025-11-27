using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyBank.Domain.Interfaces;

namespace MyBank.Domain;

public enum AccountStatus
{
    Active = 1,
    Frozen = 2,
    Closed = 3
}

public sealed class Account : IDisable
{
    [Key]
    public int AccountId { get; set; }

    [MaxLength(20)]
    [Column(TypeName = "VARCHAR")]

    public string AccountNumber { get; set; } = null!;

    public Customer Customer { get; set; } = null!;

    [Column(TypeName = "MONEY")]
    public decimal Balance { get; set; }

    public AccountStatus Status { get; set; }

    public ActivityInfo Activity { get; set; } = null!;

    public ICollection<Card> Cards { get; set; } = null!;

    public ICollection<Transaction>? TransactionsSent { get; set; }
    public ICollection<Transaction>? TransactionsRecived { get; set; }
}