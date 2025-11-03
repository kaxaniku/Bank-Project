using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBank.Domain;

public enum CardType : byte
{
    Visa = 0,
    MasterCard = 1,
    AmericanExpress = 2
}

public enum CardStatus : byte
{
    Active = 0,
    Inactive = 1,
    Suspended = 2
}

public sealed class Card
{
    [Key]
    public int CardId { get; set; }

    [MaxLength(16)]
    [Column(TypeName = "VARCHAR")]
    // TODO: We need to add unique validation for CardNumber in the DbContext configuration
    public string CardNumber { get; set; } = null!;

    public CardType CardType { get; set; }

    public CardStatus Status { get; set; }

    public DateTime ExpirationDate { get; set; }

    [MaxLength(3)]
    [Column(TypeName = "VARCHAR")]
    public string CVC { get; set; } = null!;

    public ActivityInfo Activity { get; set; } = null!;

    public Account Account { get; set; } = null!;
}
