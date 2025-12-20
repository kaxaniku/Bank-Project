namespace MyBank.API.Models.CustomerModels;

public sealed record CustomerAddressModel
{
    public string AddressLine1 { get; set; } = null!;
    public string? AddressLine2 { get; set; }
    public string ZipCode { get; set; } = null!;
    public int CityId { get; set; }
}