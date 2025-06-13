using System.ComponentModel.DataAnnotations;

namespace AutoCenter.dto
{
    public class Model
    {
        public int ModelId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Specifications { get; set; } = null!;

        [Required]
        public int Quantity { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public Category Category { get; set; } = null!;

        [Required]
        public Manufacturer Manufacturer { get; set; } = null!;

        public ICollection<Car>? Cars { get; set; }
    }
}
