namespace MyBank.API.Models.CustomerModels;

public sealed record CustomerModel
{
    public string PersonalNumber { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public byte Gender { get; set; }
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public string AddressLine1 { get; set; } = null!;
    public string? AddressLine2 { get; set; }
    public string ZipCode { get; set; } = null!;
    public int CityId { get; set; }
}