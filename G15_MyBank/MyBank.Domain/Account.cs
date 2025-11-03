using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBank.Domain;

public class Account
{
    [Key]
    public int AccountId { get; set; }

    [Required]
    [MaxLength(20)]
    [Column(TypeName = "VARCHAR")]
    public string AccountNumber { get; set; } = null!;

    [Required]
    public Customer Customer { get; set; } = null!;

    [Required]
    [Column(TypeName = "MONEY")]
    public decimal Balance { get; set; }

    public ActivityInfo Activity { get; set; } = null!;

    [Required]
    public ICollection<Card> Cards { get; set; } = null!;

    public ICollection<Transaction>? Transactions { get; set; }
}