namespace Warehouse.DTO;
public sealed class Storage
{
    public int StorageId { get; set; }
    public string Name { get; set; } = null!;
    public string AddressLine1 { get; set; } = null!;
    public string? AddressLine2 { get; set; }
    public int CityId { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}
