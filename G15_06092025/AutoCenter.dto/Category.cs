using System.ComponentModel.DataAnnotations;

namespace AutoCenter.dto
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public int Quantity { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Model>? Models { get; set; }
    }
}
