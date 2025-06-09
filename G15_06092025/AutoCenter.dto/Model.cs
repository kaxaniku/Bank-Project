using System.ComponentModel.DataAnnotations;

namespace AutoCenter.dto
{
    public class Model
    {
        public int ModelId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        //TODO: Add rest of fields;

        public bool IsActive { get; set; } = true;

        [Required]
        public Category Category { get; set; } = null!;

        [Required]
        public Manufacturer Manufacturer { get; set; } = null!;

        public ICollection<Car>? Cars { get; set; }
    }
}
