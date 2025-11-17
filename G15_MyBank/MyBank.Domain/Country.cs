using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBank.Domain;

public sealed class Country
{
    [Key]
    public int CountryId { get; set; }

    [MaxLength(3)]
    [Column(TypeName = "CHAR")]
    public string Code { get; set; } = null!;

    [MaxLength(50)]
    public string Name { get; set; } = null!;

    public ActivityInfo Activity { get; set; } = null!;

    public ICollection<City>? Cities { get; set; }
}