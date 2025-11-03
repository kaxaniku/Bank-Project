using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBank.Domain;

public class Card
{
    [Key]
    public int CardId { get; set; }

    [Required]
    [MaxLength(16)]
    [Column(TypeName = "VARCHAR")]
    public string CardNumber { get; set; } = null!;

    [Required]
    public CardTypeEnum CardType { get; set; }

    [Required]
    public CardStatus Status { get; set; }

    [Required]
    public DateTime ExpirationDate { get; set; }

    [Required]
    [MaxLength(3)]
    [Column(TypeName = "VARCHAR")]
    public string CVC { get; set; } = null!;

    public ActivityInfo Activity { get; set; } = null!;

    public Account? Account { get; set; }

    public enum CardTypeEnum
    {
        Visa,
        MasterCard,
        AmericanExpress
    }

    public enum CardStatus
    {
        Active,
        Inactive,
        Suspended
    }
}