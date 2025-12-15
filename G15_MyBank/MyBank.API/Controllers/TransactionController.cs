using Microsoft.AspNetCore.Mvc;
using MyBank.API.Models;
using MyBank.Application.Interfaces.Services;

namespace MyBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
        }

        [HttpGet("{transactionId}")]
        public async Task<IActionResult> GetTrasnaction([FromRoute] int transactionId, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] TransactionModel transactionModel, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransactionModel model, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> WithDraw([FromBody] TransactionModel model, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpGet("statement/{accountId}")]
        public async Task<IActionResult> GenerateStatement(
        [FromRoute] int accountId,
        [FromQuery] DateTime fromDate,
        [FromQuery] DateTime toDate,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

    }
}
