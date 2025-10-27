namespace BankSystem.Domain.Enums;

public enum TransactionStatus : byte
{
    Pending = 1,
    Completed = 2,
    Failed = 3,
    Reversed = 4
}
