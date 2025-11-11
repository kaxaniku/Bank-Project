using BankSystem.Application.Common.DTOs.Account;
using BankSystem.Application.Features.Auth.Commands.AccountStatus;
using BankSystem.Application.Features.Auth.Commands.CreateAccount;
using BankSystem.Application.Features.Auth.Commands.DeleteAccount;
using BankSystem.Application.Features.Auth.Commands.RestoreAccount;
using BankSystem.Application.Features.Auth.Queries.GetAccount;
using BankSystem.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class AccountController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet("{accountId:int:min(1)}")]
    [ProducesResponseType(typeof(Result<GetAccountResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAccountById(int accountId)
    {
        var response = await _mediator.Send(new GetAccountByIdQuery(accountId));
        return Ok(response);
    }

    [HttpGet("{accountNumber}")]
    [ProducesResponseType(typeof(Result<GetAccountResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAccountByNumber(string accountNumber)
    {
        var response = await _mediator.Send(new GetAccountByNumberQuery(accountNumber));
        return Ok(response);
    }

    [HttpGet("{customerId:int:min(1)}/balance")]
    [ProducesResponseType(typeof(Result<decimal>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAccountTotalBalance(int customerId)
    {
        var response = await _mediator.Send(new GetAccountBalanceQuery(customerId));
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateAccountResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateAccount(CreateAccountRequestDto request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CreateAccountCommand(request), cancellationToken);
        return Ok(response);
    }

    [HttpPost("{accountId:int:min(1)}/suspend")]
    [ProducesResponseType(typeof(CreateAccountResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SuspendAccount(int accountId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new SuspendAccountCommand(accountId), cancellationToken);
        return Ok(response);
    }

    [HttpPost("{accountId:int:min(1)}/reactivate")]
    [ProducesResponseType(typeof(CreateAccountResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ReactivateAccount(int accountId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new ReactivateAccountCommand(accountId), cancellationToken);
        return Ok(response);
    }

    [HttpPost("{accountId:int:min(1)}/close")]
    [ProducesResponseType(typeof(CreateAccountResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CloseAccount(int accountId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CloseAccountCommand(accountId), cancellationToken);
        return Ok(response);
    }

    [HttpPost("{accountId:int:min(1)}/restore")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RestoreAccount(int accountId)
    {
        var response = await _mediator.Send(new RestoreAccountCommand(accountId));
        return Ok(response);
    }

    [HttpDelete("{accountId:int:min(1)}/soft")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> DeleteAccount(int accountId)
    {
        var response = await _mediator.Send(new SoftDeleteAccountCommand(accountId));
        return Ok(response);
    }
}
