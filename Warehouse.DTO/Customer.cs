namespace Warehouse.DTO;
public sealed class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; } = null!;
    public string AddressLine1 { get; set; } = null!;
    public string? AddressLine2 { get; set; }
    public int CityId { get; set; }
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}
