namespace Warehouse.DTO;
public sealed class Country
{
    public int CountryId { get; set; }
    public string Name { get; set; } = null!;
    public string ISOCode { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}
