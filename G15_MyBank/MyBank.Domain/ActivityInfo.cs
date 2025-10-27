using System.ComponentModel.DataAnnotations.Schema;

namespace MyBank.Domain;

[ComplexType]
public class ActivityInfo
{
    public bool IsActive { get; set; } = true;
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdateDate { get; set; }
}
