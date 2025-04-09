namespace Warehouse.Services.Models;

public record TransactionResponse(
    int TransactionId,
    int ContractId,
    int EmployeeId,
    int ProductId,
    int SlotId,
    int Quantity,
    string TransactionType,
    DateTime CreateDate,
    string? CustomerAgent
);