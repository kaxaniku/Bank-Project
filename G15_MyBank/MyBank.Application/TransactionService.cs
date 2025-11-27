using MyBank.Application.Interfaces.Services;
using MyBank.Domain;

namespace MyBank.Application
{
    internal class TransactionService : ITransactionService
    {
        public IEnumerable<Transaction> GenerateStatement(int accountId, DateTime fromDate, DateTime toDate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Transaction>> GenerateStatementAsync(int accountId, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Transaction? GetTransaction(int transactionId)
        {
            throw new NotImplementedException();
        }

        public Task<Transaction?> GetTransactionAsync(int transactionId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetTransactions(int accountId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Transaction>> GetTransactionsAsync(int accountId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public bool IsTransactionAllowed(int accountId, decimal amount)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsTransactionAllowedAsync(int accountId, decimal amount, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void ProcessCardPayment(int cardId, decimal amount)
        {
            throw new NotImplementedException();
        }

        public Task ProcessCardPaymentAsync(int cardId, decimal amount, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void TransferMoney(int fromAccountId, int toAccountId, decimal amount)
        {
            throw new NotImplementedException();
        }

        public Task TransferMoneyAsync(int fromAccountId, int toAccountId, decimal amount, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
