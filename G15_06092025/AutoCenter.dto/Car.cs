using System.ComponentModel.DataAnnotations;

namespace AutoCenter.dto
{
    public class Car
    {
        public int CarId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        //TODO: Add rest of fields;

        public bool IsActive { get; set; } = true;

        [Required]
        public Model Model { get; set; } = null!;
    }
}
