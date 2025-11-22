using MyBank.Application.Interfaces.Repositories;
using MyBank.Application.Interfaces.Services;
using MyBank.Domain;

namespace MyBank.Application;

public sealed class TransactionService : ITransactionService
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public void TransferMoney(int fromAccountId, int toAccountId, decimal amount)
    {
        throw new NotImplementedException();
    }

    public void ProcessCardPayment(int cardId, decimal amount)
    {
        throw new NotImplementedException();
    }

    public bool IsTransactionAllowed(int accountId, decimal amount)
    {
        throw new NotImplementedException();
    }

    public Transaction? GetTransaction(int transactionId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Transaction> ListTransactions(int accountId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Transaction> GenerateStatement(int accountId, DateTime fromDate, DateTime toDate)
    {
        throw new NotImplementedException();
    }

    public Task TransferMoneyAsync(int fromAccountId, int toAccountId, decimal amount)
    {
        throw new NotImplementedException();
    }

    public Task ProcessCardPaymentAsync(int cardId, decimal amount)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsTransactionAllowedAsync(int accountId, decimal amount)
    {
        throw new NotImplementedException();
    }

    public Task<Transaction?> GetTransactionAsync(int transactionId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Transaction>> ListTransactionsAsync(int accountId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Transaction>> GenerateStatementAsync(int accountId, DateTime fromDate, DateTime toDate)
    {
        throw new NotImplementedException();
    }
}
