namespace MyBank.API.Models.CustomerModels;

public sealed record CustomerPrivateModel
{
    public string PersonalNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}