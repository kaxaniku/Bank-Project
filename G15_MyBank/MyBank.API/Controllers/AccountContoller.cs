using Microsoft.AspNetCore.Mvc;
using MyBank.API.Models;
using MyBank.Application.Interfaces.Services;

namespace MyBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountContoller : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountContoller(IAccountService accountService)
        {
            _accountService = accountService ?? throw new ArgumentException(nameof(accountService));
        }

        [HttpGet("AccountsByCustomer/{personalNumber}")]
        public async Task<IActionResult> GetByCustomer([FromRoute] string personalNumber, CancellationToken token)
        {
            if (personalNumber == null)
                return BadRequest("Personal number cannot be null");

            return Ok( await _accountService.GetAccountsByCustomerAsync(personalNumber, token));
        }

        [HttpPost]
        public async Task<IActionResult> OpenAccount([FromBody] AccountModel account, CancellationToken token)
        {
            if (account == null)
                return BadRequest("Account not found");

            await _accountService.OpenNewAccountAsync(
                  account.Customer.PersonalNumber,
                  account.AccountNumber,
                  account.Balance,
                  token);

            return Ok(account);
        }

        [HttpPut("CheckBalance{accoutNumber}")]
        public async Task<IActionResult> CheckAccountBalance([FromRoute] string accountNumber, CancellationToken token)
        {
            if (accountNumber == null)
                return BadRequest($"Account with number {accountNumber} not found");

            var accountBalance = await _accountService.CheckBalanceAsync(accountNumber, token);

            return Ok(accountBalance);
        }

        [HttpPut("FreezeAccount/{accountNumber}")]
        public async Task<IActionResult> Freeze(string accountNumber, CancellationToken token)
        {
            if (accountNumber == null)
                return BadRequest($"Account with number {accountNumber} not found");

            await _accountService.FreezeAccountAsync(accountNumber, token);
            return Ok("Acount deactivated succesfully");
        }

        [HttpPut("UnfreezeAccount{accountNumber}")]
        public async Task<IActionResult> Unfreeze(string accountNumber, CancellationToken token)
        {
            if (accountNumber == null)
                return BadRequest($"Account with number {accountNumber} not found");

            await _accountService.UnfreezeAccountAsync(accountNumber, token);
            return Ok("Account activated succesfully");
        }

        [HttpDelete("{accountNumber}")]
        public async Task<IActionResult> Close(string accountNumber, CancellationToken token)
        {
            if (accountNumber == null)
                return BadRequest($"Account with number {accountNumber} not found");

            await _accountService.CloseAccountAsync(accountNumber, token);
            return Ok("Account Closed succesfully");
        }
    }
}
