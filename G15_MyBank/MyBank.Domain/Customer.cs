using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBank.Domain;
public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [Required]
    [MaxLength(11), MinLength(11)]
    [Column(TypeName = "VARCHAR")]
    public string IdNum { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(30)]
    public string LastName { get; set; } = null!;

    public GenderEnum Gender { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(40)]
    public string Email { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    [Column(TypeName = "VARCHAR")]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    public DateTime? DateOfBirth { get; set; }

    [Required]
    [MaxLength(200)]
    public string Address { get; set; } = null!;

    public ActivityInfo Activity { get; set; } = null!;

    public ICollection<Account>? Accounts { get; set; }

    public enum GenderEnum
    {
        Male,
        Female,
        ProblematicMentality,
    }
}
