using System.ComponentModel.DataAnnotations;

namespace MyBank.Domain;

public class Card
{
    [Key]
    public int CardId { get; set; }

    [Required]
    [MaxLength(16)]
    public string CardNumber { get; set; } = null!;

    [Required]
    public string CardType { get; set; } = null!;

    [Required]
    public DateTime ExpirationDate { get; set; }

    [Required]
    [MaxLength(3)]
    public string CVC { get; set; } = null!;

    public ActivityInfo Activity { get; set; } = null!;

    public Account? Account { get; set; }
}