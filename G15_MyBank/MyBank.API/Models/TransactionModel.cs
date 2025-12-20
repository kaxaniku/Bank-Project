using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyBank.Domain;

namespace MyBank.API.Models;

public sealed record TransactionModel
{
    public DateTime TransactionDate { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public byte Status { get; set; }
    public byte Type { get; set; }
    public string FromAccountNumber { get; set; } = null!;
    public string ToAccountNumber { get; set; } = null!;
}