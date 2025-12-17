using Microsoft.AspNetCore.Mvc;
using MyBank.Application.Interfaces.Services;
using MyBank.API.Models;
using AutoMapper;

namespace MyBank.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;

    public AccountController(IAccountService accountService, IMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> OpenNewAccount([FromBody] AccountModel account, CancellationToken cancellationToken)
    {
        if (account == null)
        {
            return BadRequest("account is null");
        }
        await _accountService.OpenNewAccountAsync(account.CustomerId, 
            account.AccountNumber, account.Balance, cancellationToken);
        return Ok(account);
    }

    [HttpDelete("{accountNum}/close")]
    public async Task<IActionResult> CloseAccount(string accountNum, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(accountNum))
        {
            return BadRequest("accountNum is required");
        }
        await _accountService.CloseAccountAsync(accountNum, cancellationToken);
        return NoContent();
    }

    [HttpPut("{accountNum}/activate")]
    public async Task<IActionResult> ActivateAccount(string accountNum, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(accountNum))
        {
            return BadRequest("accountNum is required");
        }
        await _accountService.ActivateAccountAsync(accountNum, cancellationToken);
        return Ok("Account successfully activated.");
    }

    [HttpPut("{accountNum}/deactivate")]
    public async Task<IActionResult> DeactivateAccount(string accountNum, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(accountNum))
        {
            return BadRequest("accountNum is required");
        }
        await _accountService.DeactivateAccountAsync(accountNum, cancellationToken);
        return Ok("Account successfully deactivated.");
    }

    [HttpPut("{accountNum}/block")]
    public async Task<IActionResult> BlockAccount(string accountNum, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(accountNum))
        {
            return BadRequest("accountNum is required");
        }
        await _accountService.BlockAccountAsync(accountNum, cancellationToken);
        return Ok("Account successfully blocked.");
    }

    [HttpGet("{accountNum}/balance")]
    public async Task<IActionResult> CheckBalance(string accountNum, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(accountNum))
        {
            return BadRequest("accountNum is required");
        }
        var balance = await _accountService.CheckBalanceAsync(accountNum, cancellationToken);
        return Ok(new { Balance = balance });
    }

    [HttpGet("{accountNum}")]
    public async Task<IActionResult> FindAccount(string accountNum, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(accountNum))
        {
            return BadRequest("accountNum is required");
        }
        var account = await _accountService.FindAccountAsync(accountNum, cancellationToken);
        var accountModel = _mapper.Map<AccountModel>(account);
        return Ok(accountModel);
    }
}
