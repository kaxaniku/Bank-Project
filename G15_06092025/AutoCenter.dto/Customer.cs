using System.ComponentModel.DataAnnotations;

namespace AutoCenter.dto
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        [Required]
        public GenderType Gender { get; set; }

        public AddressInfo Address { get; set; } = new AddressInfo();

        public bool IsActive { get; set; } = true;

        [Required]
        public City City { get; set; } = null!;
    }
}
