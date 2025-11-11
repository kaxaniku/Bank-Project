namespace BankSystem.Domain.Enums;

public enum CardStatus : byte
{
    Active = 1,
    Inactive = 2,
    Blocked = 3,
    Expired = 4,
    Stolen = 5,
    Lost = 6,
    Cancelled = 7
}
