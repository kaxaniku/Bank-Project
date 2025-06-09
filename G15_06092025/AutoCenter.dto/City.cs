using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AutoCenter.dto
{
    public class City
    {
        public int CityId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [Column(TypeName = "CHAR(3)")]
        public string IsoCode { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        [Required]
        public Country Country { get; set; } = null!;
    }
}
