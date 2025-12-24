using MyBank.Domain;

namespace MyBank.API.Models
{
    public class AccountModel
    {
        public int AccountId { get; set; }
        public string AccountNumber { get; set; } = null!;
        public decimal Balance { get; set; }
        public AccountStatus Status { get; set; }
        public ActivityInfo Activity { get; set; } = null!;
        public CustomerModel Customer { get; set; } = null!;
    }
}
