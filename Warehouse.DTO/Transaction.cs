namespace Warehouse.DTO;
public sealed class Transaction
{
    public int TransactionId { get; set; }
    public int ContractId { get; set; }
    public int EmployeeId { get; set; }
    public int ProductId { get; set; }
    public int SlotId { get; set; }
    public int Quantity { get; set; }
    public string TransactionType { get; set; } = null!;
    public DateTime CreateDate { get; set; }
    public string? CustomerAgent { get; set; }
}
