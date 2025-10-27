using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBank.Domain;

public class Transaction
{
    [Key]
    public int TransactionId { get; set; }

    [Required]
    public DateTime TransactionDate { get; set; }

    [Required]
    [Column(TypeName = "MONEY")]
    public decimal Amount { get; set; }

    [MaxLength(250)]
    public string? Description { get; set; }

    [Required]
    public Account FromAccount { get; set; } = null!;

    [Required]
    public Account ToAccount { get; set; } = null!;
}