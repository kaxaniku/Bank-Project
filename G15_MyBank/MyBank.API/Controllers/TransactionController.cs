using System.Globalization;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBank.API.Models;
using MyBank.API.Models.Requests;
using MyBank.Application.Interfaces.Services;
using MyBank.Domain;

namespace MyBank.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;
    private readonly IMapper _mapper;

    public TransactionController(ITransactionService transactionService, IMapper mapper)
    {
        _transactionService = transactionService;
        _mapper = mapper;
    }

    [HttpPost("transfer")]
    public async Task<IActionResult> TransferMoney(
        [FromBody] TransferMoneyRequest request,
        CancellationToken cancellationToken)
    {
        if (request.Amount <= 0)
            return BadRequest("Transfer amount must be greater than zero.");
        if (string.IsNullOrWhiteSpace(request.FromAccountNum) ||
            string.IsNullOrWhiteSpace(request.ToAccountNum))
            return BadRequest("Both FromAccountNum and ToAccountNum are required.");
        if (request.FromAccountNum == request.ToAccountNum)
            return BadRequest("FromAccountNum and ToAccountNum cannot be the same.");

        await _transactionService.TransferMoneyAsync(
            request.FromAccountNum,
            request.ToAccountNum,
            request.Amount,
            cancellationToken);
        return Ok(new { message = "Money transferred successfully" });
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> DepositMoney(
        [FromBody] DepositWithdrawRequest request,
        CancellationToken cancellationToken)
    {
        if (request.Amount <= 0)
            return BadRequest("Deposit amount must be greater than zero.");
        if (string.IsNullOrWhiteSpace(request.AccountNum))
            return BadRequest("AccountNum is required.");

        await _transactionService.DepositMoneyAsync(
            request.AccountNum,
            request.Amount,
            cancellationToken);
        return Ok(new { message = "Money deposited successfully" });
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> WithdrawMoney(
        [FromBody] DepositWithdrawRequest request,
        CancellationToken cancellationToken)
    {
        if (request.Amount <= 0)
            return BadRequest("Withdraw amount must be greater than zero.");
        if (string.IsNullOrWhiteSpace(request.AccountNum))
            return BadRequest("AccountNum is required.");

        await _transactionService.WithdrawMoneyAsync(
            request.AccountNum,
            request.Amount,
            cancellationToken);
        return Ok(new { message = "Money withdrawn successfully" });
    }

    [HttpPost("cardpayment")]
    public async Task<IActionResult> ProcessCardPayment(
        [FromBody] CardPaymentRequest request,
        CancellationToken cancellationToken)
    {
        if (request.Amount <= 0)
            return BadRequest("Payment amount must be greater than zero.");
        if (string.IsNullOrWhiteSpace(request.CardNum) ||
            string.IsNullOrWhiteSpace(request.ReceiverAccountNum))
            return BadRequest("Both CardNum and ReceiverAccountNum are required.");

        await _transactionService.ProcessCardPaymentAsync(
            request.CardNum,
            request.ReceiverAccountNum,
            request.Amount,
            cancellationToken);
        return Ok(new { message = "Card payment processed successfully" });
    }

    [HttpGet("{transactionId}")]
    public async Task<IActionResult> GetTransaction(int transactionId, CancellationToken cancellationToken)
    {
        var transaction = await _transactionService.GetTransactionAsync(transactionId, cancellationToken);
        var transactionModel = _mapper.Map<TransactionModel>(transaction);
        return Ok(transactionModel);
    }

    [HttpGet("list/{type}")]
    public async Task<IActionResult> ListTransactions(TransactionType type,
        [FromQuery] int pageNumber = 1,
        CancellationToken cancellationToken = default)
    {
        var transactions = await _transactionService.ListTransactionsAsync(type, cancellationToken, pageNumber);
        var transactionModels = _mapper.Map<IEnumerable<TransactionModel>>(transactions);
        return Ok(transactionModels);
    }

    [HttpGet("statementList/{accountNum}")]
    public async Task<IActionResult> GenerateStatement(string accountNum,
        [FromQuery] string fromDate,
        [FromQuery] string toDate,
        [FromQuery] int pageNumber = 1,
        CancellationToken cancellationToken = default)
    {
        DateTime from = DateTime.ParseExact(fromDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        DateTime to = DateTime.ParseExact(toDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

        var transactions = await _transactionService.GenerateStatementAsync(
            accountNum, from, to, cancellationToken, pageNumber);
        var transactionModels = _mapper.Map<IEnumerable<TransactionModel>>(transactions);
        return Ok(transactionModels);
    }
}
