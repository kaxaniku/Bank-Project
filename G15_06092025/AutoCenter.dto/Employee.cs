using System.ComponentModel.DataAnnotations;

namespace AutoCenter.dto;

public abstract class Employee
{
    public int EmployeeId { get; set; }

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

public class Driver : Employee
{
    [Required]
    [MaxLength(20)]
    public string LicenseNumber { get; set; } = null!;

    [Required]
    public DateTime LicenseExpiryDate { get; set; }
}

public class Administrator : Employee
{
    [Required]
    [MaxLength(50)]
    public string Position { get; set; } = null!;

    [Required]
    public DateTime HireDate { get; set; }
}

public class Mechanic : Employee
{
    [Required]
    [MaxLength(50)]
    public string Specialization { get; set; } = null!;

    [Required]
    public DateTime HireDate { get; set; }
}