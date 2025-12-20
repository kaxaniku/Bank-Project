namespace MyBank.API.Models.CustomerModels;
public sealed record CustomerDisplayModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public byte Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
}