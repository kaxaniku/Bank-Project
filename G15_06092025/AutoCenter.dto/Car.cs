using System.ComponentModel.DataAnnotations;

namespace AutoCenter.dto
{
    public class Car
    {
        public int CarId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string Color { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string Number { get; set; } = null!;

        [Required]
        public int Horsepower { get; set; }

        [Required]
        public int Quantity { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public Model Model { get; set; } = null!;
    }
}
