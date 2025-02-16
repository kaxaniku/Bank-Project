namespace Warehouse.DTO;
public sealed class Position
{
    public int PositionId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal? Salary { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}