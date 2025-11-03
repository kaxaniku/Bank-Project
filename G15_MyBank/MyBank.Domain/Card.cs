using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBank.Domain;

public class Card
{
    public enum CardTypeEnum
    {
        Visa,
        MasterCard,
        AmericanExpress
    }

    public enum CardLifecycleStatus
    {
        PendingActivation,
        Active,
        TemporarilySuspended,
        Closed,
        Replaced
    }

    [Key]
    public int CardId { get; set; }

    [Required]
    [MaxLength(16)]
    [Column(TypeName = "VARCHAR")]
    public string CardNumber { get; set; } = null!;

    [Required]
    public CardTypeEnum CardType { get; set; }

    [Required]
    public CardLifecycleStatus Status { get; set; }

    [Required]
    [Range(1, 12)]
    public int ExpiryMonth { get; set; }

    [Required]
    [Range(2025, 2100)]
    public int ExpiryYear { get; set; }

    [Required]
    [MaxLength(3)]
    [Column(TypeName = "VARCHAR")]
    public string CVC { get; set; } = null!;

    public ActivityInfo Activity { get; set; } = null!;

    public Account? Account { get; set; }
}