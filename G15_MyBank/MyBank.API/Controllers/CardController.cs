using Microsoft.AspNetCore.Mvc;
using MyBank.API.Models;
using MyBank.Application.Interfaces.Services;

namespace MyBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService ?? throw new ArgumentNullException(nameof(cardService));
        }

        [HttpGet("Cards/{accountNumber}")]
        public async Task<IActionResult> GetByAccount([FromRoute] string accountNumber, CancellationToken token)
        {
            if (accountNumber == null)
                return BadRequest($"No account or associated cards found for account number: {accountNumber}");

            return Ok(await _cardService.GetCardsByAccountAsync(accountNumber, token));
        }

        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] CardModel card, CancellationToken token)
        {
            if (card == null)
                return BadRequest("Card is null");

            await _cardService.AddNewCardAsync(
                  card.CardNumber,
                  card.CardType,
                  card.ExpirationDate,
                  card.CVC,
                  card.Account.AccountId,
                  token);

            return Ok("Card added sucesfully");
        }

        [HttpPut("BlockCard{cardNumber}")]
        public async Task<IActionResult> Block([FromRoute] string cardNumber, CancellationToken token)
        {
            if (cardNumber == null)
                return BadRequest($"Card with cardnumber {cardNumber} not found");

            await _cardService.BlockCardAsync(cardNumber, token);
            return Ok("Card blocked sucesfully");
        }

        [HttpPut("UnblockCard/{cardNumber}")]
        public async Task<IActionResult> UnBlock([FromRoute] string cardNumber, CancellationToken token)
        {
            if (cardNumber == null)
                return BadRequest($"Card with cardnumber {cardNumber} not found");

            await _cardService.UnblockCardAsync(cardNumber, token);
            return Ok("Card unblocked sucesfully");
        }

        [HttpPut("ActivateCard/{cardNumber}")]
        public async Task<IActionResult> Activate([FromRoute] string cardNumber, CancellationToken token)
        {
            if (cardNumber == null)
                return BadRequest($"Card with cardnumber {cardNumber} not found");

            await _cardService.ActivateCardAsync(cardNumber, token);
            return Ok("Card activated sucesfully");
        }
    }
}
