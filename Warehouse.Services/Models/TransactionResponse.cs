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

//TODO: Delete this class
public record Person(string FirstName, string LastName)
{
    public string FullName => $"{FirstName} {LastName}";
}