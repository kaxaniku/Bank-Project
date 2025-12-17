using Microsoft.AspNetCore.Mvc;
using MyBank.API.Models;
using MyBank.Application.Interfaces.Services;

namespace MyBank.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService ?? throw new ArgumentNullException(nameof(cardService));
        }

        [HttpGet("{cardNumber}")]
        public async Task<IActionResult> FindCard(string cardNumber, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{accountNumber}/list")]
        public async Task<IActionResult> GetCardsByAccount([FromRoute] string accountNumber, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] CardModel card, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{cardNumber}/close")]
        public async Task<IActionResult> CloseAccount(string cardNumber, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{cardNumber}/activate")]
        public async Task<IActionResult> ActivateCard([FromRoute] string cardNumber, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{cardNumber}/deactivate")]
        public async Task<IActionResult> DeactivateCard([FromRoute] string cardNumber, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{cardNumber}/suspend")]
        public async Task<IActionResult> SuspendCard([FromRoute] string cardNumber, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}