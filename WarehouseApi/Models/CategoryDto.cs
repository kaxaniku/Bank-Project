using System.ComponentModel.DataAnnotations;

namespace WarehouseApi.Models;

public class CategoryDto
{
    [Required]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}
