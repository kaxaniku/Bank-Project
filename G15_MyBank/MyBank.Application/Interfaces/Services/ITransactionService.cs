using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services
{
    internal interface ITransactionService 
    {
        IEnumerable<Transaction> GenerateStatement(int accountId, DateTime fromDate, DateTime toDate);
        Task<IEnumerable<Transaction>> GenerateStatementAsync(int accountId, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken);
        Transaction? GetTransaction(int transactionId);
        Task<Transaction?> GetTransactionAsync(int transactionId, CancellationToken cancellationToken);
        bool IsTransactionAllowed(int accountId, decimal amount);
        Task<bool> IsTransactionAllowedAsync(int accountId, decimal amount, CancellationToken cancellationToken);
        IEnumerable<Transaction> GetTransactions(int accountId);
        Task<IEnumerable<Transaction>> GetTransactionsAsync(int accountId, CancellationToken cancellationToken);
        void ProcessCardPayment(int cardId, decimal amount);
        Task ProcessCardPaymentAsync(int cardId, decimal amount, CancellationToken cancellationToken);
        void TransferMoney(int fromAccountId, int toAccountId, decimal amount);
        Task TransferMoneyAsync(int fromAccountId, int toAccountId, decimal amount, CancellationToken cancellationToken);
    }
}
