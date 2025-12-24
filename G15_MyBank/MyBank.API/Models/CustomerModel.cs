using MyBank.Domain;

namespace MyBank.API.Models
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }
        public string PersonalNumber { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public Gender Gender { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string AdressLine1 { get; set; } = null!;
        public string? AdressLine2 { get; set; }
        public string ZipCode { get; set; } = null!;
        public CityModel City { get; set; } = null!;
    }
}
