using MyBank.Domain;

namespace MyBank.API.Models
{
    public class CardModel
    {
        public int CardId { get; set; }
        public string CardNumber { get; set; } = null!;
        public CardType CardType { get; set; }
        public CardStatus Status { get; set; }
        public string CVC { get; set; } = null!;
        public DateTime ExpirationDate { get; set; }
        public AccountModel Account { get; set; } = null!;
    }
}
