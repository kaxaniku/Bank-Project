using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBank.API.Models;
using MyBank.Application.Interfaces.Services;

namespace MyBank.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CardController : ControllerBase
{
    private readonly ICardService _cardService;
    private readonly IMapper _mapper;

    public CardController(ICardService cardService, IMapper mapper)
    {
        _cardService = cardService ?? throw new ArgumentNullException(nameof(cardService));
        _mapper = mapper;
    }

    [HttpGet("{cardNumber}")]
    public async Task<IActionResult> GetCard(string cardNumber, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(cardNumber))
            return BadRequest("cardNumber is required");
        var card = await _cardService.FindCardAsync(cardNumber, cancellationToken);
        var cardModel = _mapper.Map<CardModel>(card);
        return Ok(cardModel);
    }

    [HttpGet("list/{accountNumber}")]
    public async Task<IActionResult> GetCardsByAccount([FromRoute] string accountNumber, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(accountNumber))
            return BadRequest("accountNumber is required");
        var cards = await _cardService.ListCardsByAccountAsync(accountNumber, cancellationToken);
        var cardModels = _mapper.Map<IEnumerable<CardModel>>(cards);
        return Ok(cardModels);
    }

    [HttpPost]
    public async Task<IActionResult> AddCard([FromBody] CardModel? card, CancellationToken cancellationToken)
    {
        if (card == null)
            return BadRequest("Card is null");
        await _cardService.IssueNewCardAsync(
                card.CardNumber,
                card.CardType,
                card.CVC,
                card.ExpirationDate,
                card.AccountNumber,
                cancellationToken);
        return Ok(new { card, message = "Card successfully issued." });
    }

    [HttpDelete("close/{cardNumber}")]
    public async Task<IActionResult> CloseCard(string cardNumber, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(cardNumber))
            return BadRequest("cardNumber is required");
        await _cardService.CloseCardAsync(cardNumber, cancellationToken);
        return NoContent();
    }

    [HttpPut("activate/{cardNumber}")]
    public async Task<IActionResult> ActivateCard([FromRoute] string cardNumber, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(cardNumber))
            return BadRequest("cardNumber is required");
        await _cardService.ActivateCardAsync(cardNumber, cancellationToken);
        return Ok("Card successfully activated.");
    }

    [HttpPut("deactivate/{cardNumber}")]
    public async Task<IActionResult> DeactivateCard([FromRoute] string cardNumber, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(cardNumber))
            return BadRequest("cardNumber is required");
        await _cardService.DeactivateCardAsync(cardNumber, cancellationToken);
        return Ok("Card successfully deactivated.");
    }

    [HttpPut("suspend/{cardNumber}")]
    public async Task<IActionResult> SuspendCard([FromRoute] string cardNumber, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(cardNumber))
            return BadRequest("cardNumber is required");
        await _cardService.SuspendCardAsync(cardNumber, cancellationToken);
        return Ok("Card successfully suspended.");
    }
}