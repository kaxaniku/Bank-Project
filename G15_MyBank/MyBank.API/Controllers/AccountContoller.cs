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

        [HttpGet("accounts/{personalNumber}")]
        public async Task<IActionResult> GetByCustomer([FromRoute] string personalNumber, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> OpenAccount([FromBody] AccountModel account, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpPut("account/{accoutNumber}/balance")]
        public async Task<IActionResult> CheckAccountBalance([FromRoute] string accountNumber, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpPut("account/by-number/{accountNumber}/freeze")]
        public async Task<IActionResult> Freeze([FromBody] AccountModel account, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpPut("account/by-number/{accountNumber}/unfreeze")]
        public async Task<IActionResult> Unfreeze([FromBody] AccountModel account, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{accountNumber}")]
        public async Task<IActionResult> Close([FromRoute] string accountNumber, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
