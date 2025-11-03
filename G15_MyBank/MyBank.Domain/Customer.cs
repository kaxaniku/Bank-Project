using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBank.Domain;

public enum Gender : byte
{
    Male = 0,
    Female = 1,
    Other = 2,
}

public sealed class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [StringLength(11, MinimumLength = 11)]
    [Column(TypeName = "VARCHAR")]
    // TODO: We need to add unique validation for PersonalNumber in the DbContext configuration
    public string PersonalNumber { get; set; } = null!;

    [MaxLength(20)]
    public string FirstName { get; set; } = null!;

    [MaxLength(30)]
    public string LastName { get; set; } = null!;

    public Gender Gender { get; set; }

    [EmailAddress]
    [MaxLength(40)]
    public string Email { get; set; } = null!;

    [MaxLength(20)]
    [Column(TypeName = "VARCHAR")]
    public string PhoneNumber { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public AddressInfo Address { get; set; } = null!;

    public City City { get; set; } = null!;

    public ActivityInfo Activity { get; set; } = null!;

    public ICollection<Account>? Accounts { get; set; }
}