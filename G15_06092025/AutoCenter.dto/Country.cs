using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoCenter.dto
{
    public class Country
    {
        public int CountryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [Column(TypeName = "CHAR(3)")]
        public string IsoCode { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public ICollection<City>? Cities { get; set; }

        public ICollection<Manufacturer>? Manufacturer { get; set; }
    }
}
