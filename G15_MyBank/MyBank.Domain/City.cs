using MyBank.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MyBank.Domain;

public sealed class City : IDisable
{
    [Key]
    public int CityId { get; set; }

    [MaxLength(50)]
    public string Name { get; set; } = null!;

    public ActivityInfo Activity { get; set; } = null!;

    public Country Country { get; set; } = null!;
}