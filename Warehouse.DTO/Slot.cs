namespace Warehouse.DTO;
public sealed class Slot
{
    public int SlotId { get; set; }
    public int StorageId { get; set; }
    public string SlotCode { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}
