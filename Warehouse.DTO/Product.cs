namespace Warehouse.DTO;
public sealed class Product
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public string Barcode { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Dimensions { get; set; } = null!;
    public float Weight { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}
