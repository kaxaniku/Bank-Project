using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services
{
    public interface ITransactionService 
    {
        IEnumerable<Transaction> GenerateStatement(int accountId, DateTime fromDate, DateTime toDate, int pageNumber, int pageSize);
        Task<IEnumerable<Transaction>> GenerateStatementAsync(int accountId, DateTime fromDate, DateTime toDate, CancellationToken token, int pageNumber, int pageSize);
        Transaction? GetTransaction(int transactionId);
        Task<Transaction?> GetTransactionAsync(int transactionId, CancellationToken token);
        bool IsTransactionAllowed(int fromAccountId, int toAccountId, decimal amount);
        Task<bool> IsTransactionAllowedAsync(int fromAccountId, int toAccountId, decimal amount, CancellationToken token);
        IEnumerable<Transaction> GetTransactions(int accountId, int pageNumber, int pageSize);
        Task<IEnumerable<Transaction>> GetTransactionsAsync(int accountId, CancellationToken token, int pageNumber, int pageSize);
        void ProcessCardPayment(int cardId, int reciverId, decimal amount);
        Task ProcessCardPaymentAsync(int cardId, int reciverId, decimal amount, CancellationToken token);
        void TransferMoney(int fromAccountId, int toAccountId, decimal amount);
        Task TransferMoneyAsync(int fromAccountId, int toAccountId, decimal amount, CancellationToken token);
        void Deposit(int accountId, decimal amount);
        Task DepositAsync(int accountId, decimal amount, CancellationToken token);
        void Withdraw(int accountId, decimal amount);
        Task WithdrawAsync(int accountId, decimal amount, CancellationToken token);
    }
}
