using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AutoCenter.dto
{
    public class Manufacturer
    {
        public int ManufacturerId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        [Required]
        public Country Country { get; set; } = null!;

        public ICollection<Model>? Models { get; set; }
    }
}
