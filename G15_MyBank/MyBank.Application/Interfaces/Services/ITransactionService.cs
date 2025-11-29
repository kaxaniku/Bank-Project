using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services;

public interface ITransactionService
{
    IEnumerable<Transaction> GenerateStatement(int accountId, DateTime fromDate, DateTime toDate);
    Task<IEnumerable<Transaction>> GenerateStatementAsync(int accountId, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken);
    Transaction? GetTransaction(int transactionId);
    Task<Transaction?> GetTransactionAsync(int transactionId, CancellationToken cancellationToken);
    IEnumerable<Transaction> ListTransactions(int accountId, TransactionType type);
    Task<IEnumerable<Transaction>> ListTransactionsAsync(int accountId, TransactionType type ,CancellationToken cancellationToken);
    void ProcessCardPayment(int cardId, int recieverId, decimal amount);
    Task ProcessCardPaymentAsync(int cardId, int recieverId, decimal amount, CancellationToken cancellationToken);
    void TransferMoney(int fromAccountId, int toAccountId, decimal amount);
    Task TransferMoneyAsync(int fromAccountId, int toAccountId, decimal amount, CancellationToken cancellationToken);
    void DepositMoney(int toAccountId, decimal amount);
    Task DepositMoneyAsync(int toAccountId, decimal amount, CancellationToken cancellationToken);
    void WithdrawMoney(int fromAccountId, decimal amount);
    Task WithdrawMoneyAsync(int fromAccountId, decimal amount, CancellationToken cancellationToken);
}