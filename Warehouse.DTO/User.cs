namespace Warehouse.DTO;
public sealed class User
{
    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public byte UserRole { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}