namespace Warehouse.DTO;
public sealed class ContractDetail
{
    public int ContractDetailId { get; set; }
    public int ContractId { get; set; }
    public int SlotId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}