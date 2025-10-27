using BankSystem.Domain.Enums;

namespace BankSystem.Application.Common.DTOs;

public class CreateCustomerRequestDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string NationalId { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public CustomerType Type { get; set; }
}
