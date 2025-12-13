using Microsoft.AspNetCore.Mvc;
using MyBank.Application.Interfaces.Services;
using MyBank.API.Models;

namespace MyBank.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost]
    public IActionResult OpenNewAccount([FromBody] AccountModel account, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{accountNum}")]
    public IActionResult CloseAccount(string accountNum, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{accountNum}/activate")]
    public IActionResult ActivateAccount(string accountNum, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{accountNum}/deactivate")]
    public IActionResult DeactivateAccount(string accountNum, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{accountNum}/block")]
    public IActionResult BlockAccount(string accountNum, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{accountNum}/balance")]
    public IActionResult CheckBalance(string accountNum, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{accountNum}")]
    public IActionResult FindAccount(string accountNum, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
