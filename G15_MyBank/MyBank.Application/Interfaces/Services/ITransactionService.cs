using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services;

public interface ITransactionService
{
    IEnumerable<Transaction> GenerateStatement(string toAccountNum, DateTime fromDate, DateTime toDate, int pageNumber = 1);
    Task<IEnumerable<Transaction>> GenerateStatementAsync(string toAccountNum, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken, int pageNumber = 1);
    Transaction? GetTransaction(int transactionId);
    Task<Transaction?> GetTransactionAsync(int transactionId, CancellationToken cancellationToken);
    IEnumerable<Transaction> ListTransactions(string toAccountNum, TransactionType type);
    Task<IEnumerable<Transaction>> ListTransactionsAsync(string toAccountNum, TransactionType type ,CancellationToken cancellationToken);
    void ProcessCardPayment(int cardId, string recieverNum, decimal amount);
    Task ProcessCardPaymentAsync(int cardId, string recieverNum, decimal amount, CancellationToken cancellationToken);
    void TransferMoney(string fromAccountNum, string toAccountNum, decimal amount);
    Task TransferMoneyAsync(string fromAccountNum, string toAccountNum, decimal amount, CancellationToken cancellationToken);
    void DepositMoney(string toAccountNum, decimal amount);
    Task DepositMoneyAsync(string toAccountNum, decimal amount, CancellationToken cancellationToken);
    void WithdrawMoney(string fromAccountNum, decimal amount);
    Task WithdrawMoneyAsync(string fromAccountNum, decimal amount, CancellationToken cancellationToken);
}