using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBank.Domain;

[ComplexType]
public sealed class AddressInfo
{
    [MaxLength(200)]
    public string AddressLine1 { get; set; } = null!;

    [MaxLength(200)]
    public string? AddressLine2 { get; set; }

    public string ZipCode { get; set; } = null!;
}