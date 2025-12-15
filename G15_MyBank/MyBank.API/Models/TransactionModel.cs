using MyBank.Domain;

namespace MyBank.API.Models
{
    public class TransactionModel
    {
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Description { get; set; }
        public TransactionStatus Status { get; set; }
    }
}
