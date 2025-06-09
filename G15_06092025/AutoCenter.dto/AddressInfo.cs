using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoCenter.dto
{
    [ComplexType]
    public class AddressInfo
    {
        [Required]
        [MaxLength(100)]
        public string Line1 { get; set; } = null!;

        [MaxLength(100)]
        public string? Line2 { get; set; }

        [MaxLength(10)]
        public string? PostalCode { get; set; }
    }
}
