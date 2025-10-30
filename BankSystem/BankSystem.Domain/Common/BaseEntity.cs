namespace BankSystem.Domain.Common;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; protected set; } = true;
    public DateTime? DeletedAt { get; protected set; }

    public virtual void SoftDelete()
    {
        IsActive = false;
        DeletedAt = DateTime.UtcNow;
    }

    public virtual void Restore()
    {
        IsActive = true;
        DeletedAt = null;
    }
}
