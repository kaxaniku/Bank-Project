namespace Warehouse.DTO;
public sealed class Contract
{
    public int ContractId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int CustomerId { get; set; }
    public int EmployeeId { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}
