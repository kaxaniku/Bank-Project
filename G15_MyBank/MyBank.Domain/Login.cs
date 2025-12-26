using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyBank.Domain.Interfaces;

namespace MyBank.Domain;

public sealed class Login : IDisable
{
    [Key]
    public int UserId { get; set; }
    [Required]
    [MaxLength(30)]
    [Column(TypeName = "VARCHAR")]
    public string Username { get; set; } = null!;
    [Required]
    public string PasswordHash { get; set; } = null!;
    public ActivityInfo Activity { get; set; } = null!;
}