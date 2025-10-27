using System.ComponentModel.DataAnnotations;

namespace MyBank.Domain;
public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = null!;

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    public DateTime? DateOfBirth { get; set; }

    [Required]
    [MaxLength(200)]
    public string Address { get; set; } = null!;

    public ActivityInfo Activity { get; set; } = null!;

    public ICollection<Account>? Accounts { get; set; }
}
