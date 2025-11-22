using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services;

public interface ITransactionService
{
    IEnumerable<Transaction> GenerateStatement(int accountId, DateTime fromDate, DateTime toDate);
    Task<IEnumerable<Transaction>> GenerateStatementAsync(int accountId, DateTime fromDate, DateTime toDate);
    Transaction? GetTransaction(int transactionId);
    Task<Transaction?> GetTransactionAsync(int transactionId);
    bool IsTransactionAllowed(int accountId, decimal amount);
    Task<bool> IsTransactionAllowedAsync(int accountId, decimal amount);
    IEnumerable<Transaction> ListTransactions(int accountId);
    Task<IEnumerable<Transaction>> ListTransactionsAsync(int accountId);
    void ProcessCardPayment(int cardId, decimal amount);
    Task ProcessCardPaymentAsync(int cardId, decimal amount);
    void TransferMoney(int fromAccountId, int toAccountId, decimal amount);
    Task TransferMoneyAsync(int fromAccountId, int toAccountId, decimal amount);
}