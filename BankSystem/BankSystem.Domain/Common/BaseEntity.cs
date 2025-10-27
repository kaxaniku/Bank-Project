namespace BankSystem.Domain.Common;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; }
    public bool IsActive { get; protected set; } = true;
}
