using MyBank.Domain;

namespace MyBank.API.Models
{
    public class CardModel
    {
        public int CardId { get; set; }
        public string CardNumber { get; set; } = null!;
        public CardType CardType { get; set; }
        public CardStatus Status { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
