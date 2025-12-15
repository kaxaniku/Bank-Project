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

        [HttpGet("cards/{accountNumber}")]
        public async Task<IActionResult> GetByAccount([FromRoute] string accountNumber, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] CardModel card, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpPut("card/by-number/{cardNumber}/block")]
        public async Task<IActionResult> Block([FromRoute] string cardNumber, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpPut("card/by-number/{cardNumber}/unblock")]
        public async Task<IActionResult> UnBlock([FromRoute] string cardNumber, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpPut("card/by-number/{cardNumber}/activate")]
        public async Task<IActionResult> Activate([FromRoute] string cardNumber, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
