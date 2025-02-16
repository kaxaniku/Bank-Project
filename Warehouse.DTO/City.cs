namespace Warehouse.DTO;
public sealed class City
{
    public int CityId { get; set; }
    public int CountryId { get; set; }
    public string Name { get; set; } = null!;
    public string PostCode { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}
