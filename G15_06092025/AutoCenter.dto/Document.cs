using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoCenter.dto;

public class Document
{
    [ForeignKey(nameof(Car))]
    public int DocumentId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Title { get; set; } = null!;

    [MaxLength(500)]
    public string? Description { get; set; }
        
    public bool IsActive { get; set; } = true;

    [Required]
    public Car Car { get; set; } = null!;
}