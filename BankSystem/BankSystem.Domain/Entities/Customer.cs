using BankSystem.Domain.Common;
using BankSystem.Domain.Enums;

namespace BankSystem.Domain.Entities;

public class Customer : BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string NationalId { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public CustomerType Type { get; set; }

    public ICollection<Account> Accounts { get; set; } = [];
}
