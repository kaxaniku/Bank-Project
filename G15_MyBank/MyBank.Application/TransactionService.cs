using MyBank.Application.Interfaces.Repositories;
using MyBank.Application.Interfaces.Services;
using MyBank.Domain;

namespace MyBank.Application
{
    internal class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public event Action<Transaction>? TransactionProcessed;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Deposit(int toAccountId, decimal amount)
        {
            Account account = _unitOfWork.AccountRepository.GetById(toAccountId)!;

            if (account == null)
                throw new KeyNotFoundException($"Account with Id {account} not found");

            if (amount <= 0)
                throw new ArgumentNullException("Amount must be positive to deposit");

            if (account.Status != AccountStatus.Active)
                throw new InvalidOperationException("Account must be active to deposit");

            account!.Balance += amount;

            Transaction transaction = new()
            {
                ToAccountId = toAccountId,
                Amount = amount,
                Description = $"Deposit {amount} on account {account.AccountNumber}",
                TransactionDate = DateTime.UtcNow
            };

            _unitOfWork.TransactionRepository.Insert(transaction);
            _unitOfWork.AccountRepository.Update(account);
            _unitOfWork.SaveChanges();
            OnTransactionProcessed(transaction);
        }

        public async Task DepositAsync(int accountId, decimal amount, CancellationToken token)
        {
            Account account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId, token);

            if (account == null)
                throw new KeyNotFoundException($"Account with Id {account} not found");

            if (amount <= 0)
                throw new ArgumentNullException("Amount must be positive to deposit");

            if (account.Status != AccountStatus.Active)
                throw new InvalidOperationException("Account must be active to deposit");

            account!.Balance += amount;

            Transaction transaction = new()
            {
                ToAccountId = accountId,
                Amount = amount,
                Description = $"Deposit {amount} on account {account.AccountNumber}",
                TransactionDate = DateTime.UtcNow
            };

            await _unitOfWork.TransactionRepository.InsertAsync(transaction, token);
            await _unitOfWork.AccountRepository.UpdateAsync(account, token);
            await _unitOfWork.SavechangesAsync(token);
            OnTransactionProcessed(transaction);
        }

        public IEnumerable<Transaction> GenerateStatement(int accountId, DateTime fromDate, DateTime toDate)
        {
            return _unitOfWork.TransactionRepository.Query(t =>
            (t.FromAccountId == accountId || 
            t.ToAccountId == accountId) &&
            t.TransactionDate >= fromDate &&
            t.TransactionDate < toDate).
            OrderBy(t => t.TransactionDate).ToList();
        }

        public async Task<IEnumerable<Transaction>> GenerateStatementAsync(int accountId, DateTime fromDate, DateTime toDate, CancellationToken token)
        {
            return await _unitOfWork.TransactionRepository.QueryAsync(t =>
            (t.FromAccountId == accountId ||
            t.ToAccountId == accountId) &&
            t.TransactionDate >= fromDate &&
            t.TransactionDate < toDate, token);
        }

        public Transaction? GetTransaction(int transactionId)
        {
            return _unitOfWork.TransactionRepository.GetById(transactionId);
        }

        public async Task<Transaction?> GetTransactionAsync(int transactionId, CancellationToken token)
        {
            return await _unitOfWork.TransactionRepository.GetByIdAsync(transactionId, token);
        }

        public IEnumerable<Transaction> GetTransactions(int accountId)
        {
            return _unitOfWork.TransactionRepository.Query(t => t.ToAccountId == accountId || t.FromAccountId == accountId).ToList();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync(int accountId, CancellationToken token)
        {
            return await _unitOfWork.TransactionRepository.QueryAsync(t => t.ToAccountId == accountId || t.FromAccountId == accountId, token);
        }

        public bool IsTransactionAllowed(int fromAccountId, int toAccountId, decimal amount)
        {
            Account fromAccount = _unitOfWork.AccountRepository.GetById(fromAccountId)!;
            if (fromAccount == null)
                throw new KeyNotFoundException($"Account with Id {fromAccountId} not found");

            Account toAccount = _unitOfWork.AccountRepository.GetById(toAccountId)!;
            if (toAccount == null)
                throw new KeyNotFoundException($"Account with Id {toAccountId} not found");

            if (amount <= 0)
                return false;

            if (fromAccount!.Status == AccountStatus.Frozen || fromAccount.Status == AccountStatus.Closed
                || toAccount.Status == AccountStatus.Frozen || toAccount.Status == AccountStatus.Closed)
                return false;

            return true;
        }

        public async Task<bool> IsTransactionAllowedAsync(int fromAccountId, int toAccountId, decimal amount, CancellationToken token)
        {
            Account fromAccount = await _unitOfWork.AccountRepository.GetByIdAsync(fromAccountId, token);
            if (fromAccount == null)
                throw new KeyNotFoundException($"Account with Id {fromAccountId} not found");

            Account toAccount = await _unitOfWork.AccountRepository.GetByIdAsync(toAccountId, token);
            if (toAccount == null)
                throw new KeyNotFoundException($"Account with Id {toAccountId} not found");

            if (amount <= 0)
                return false;

            if (fromAccount!.Status == AccountStatus.Frozen || fromAccount.Status == AccountStatus.Closed
                || toAccount.Status == AccountStatus.Frozen || toAccount.Status == AccountStatus.Closed)
                return false;

            return true;
        }

        public void ProcessCardPayment(int cardId, int recieverId,  decimal amount)
        {
            Card card = _unitOfWork.CardRepository.GetById(cardId)!;
            if (card == null)
                throw new KeyNotFoundException($"Card with Id {cardId} not found");

            Account reciever = _unitOfWork.AccountRepository.GetById(recieverId)!;
            if (reciever == null)
                throw new KeyNotFoundException($"Account with Id {recieverId} not found");

            Account account = card.Account;

            if (account.AccountId == recieverId)
                throw new InvalidOperationException("Cannot transfer money to same account");

            if (amount <= 0)
                throw new InvalidOperationException("Amount must be positive number");

            if (account.Status != AccountStatus.Active || reciever.Status != AccountStatus.Active)
                throw new InvalidOperationException("Both account must be active to transfer");

            if (account.Balance < amount)
                throw new InvalidOperationException("There is no enough amount on your balance to transfer");

            if (card.Status != CardStatus.Active)
                throw new InvalidOperationException("Card must be active to transfer");

            account.Balance -= amount;
            reciever.Balance += amount;

            Transaction transaction = new()
            {
                FromAccountId = account.AccountId,
                ToAccountId = recieverId,
                Amount = amount,
                Description = $"Card payment from card {card.CardNumber}",
                TransactionDate = DateTime.UtcNow
            };

            _unitOfWork.TransactionRepository.Insert(transaction);
            _unitOfWork.AccountRepository.Update(account);
            _unitOfWork.AccountRepository.Update(reciever);
            _unitOfWork.SaveChanges();
            OnTransactionProcessed(transaction);
        }

        public async Task ProcessCardPaymentAsync(int cardId, int recieverId, decimal amount, CancellationToken token)
        {
            Card card = await _unitOfWork.CardRepository.GetByIdAsync(cardId, token);
            if (card == null)
                throw new KeyNotFoundException($"Card with Id {cardId} not found");

            Account reciever = await _unitOfWork.AccountRepository.GetByIdAsync(recieverId, token)!;
            if (reciever == null)
                throw new KeyNotFoundException($"Account with Id {recieverId} not found");

            Account account = card.Account;

            if (account.AccountId == recieverId)
                throw new InvalidOperationException("Cannot transfer money to same account");

            if (amount <= 0)
                throw new InvalidOperationException("Amount must be positive number");

            if (account.Status != AccountStatus.Active || reciever.Status != AccountStatus.Active)
                throw new InvalidOperationException("Both account must be active to transfer");

            if (account.Balance < amount)
                throw new InvalidOperationException("There is no enough amount on your balance to transfer");

            if (card.Status != CardStatus.Active)
                throw new InvalidOperationException("Card must be active to transfer");

            account.Balance -= amount;
            reciever.Balance += amount;

            Transaction transaction = new()
            {
                FromAccountId = account.AccountId,
                ToAccountId = recieverId,
                Amount = amount,
                Description = $"Card payment from card {card.CardNumber}",
                TransactionDate = DateTime.UtcNow
            };

            await _unitOfWork.TransactionRepository.InsertAsync(transaction, token);
            await _unitOfWork.AccountRepository.UpdateAsync(account, token);
            await _unitOfWork.AccountRepository.UpdateAsync(reciever, token);
            await _unitOfWork.SavechangesAsync(token);
            OnTransactionProcessed(transaction);
        }

        public void TransferMoney(int fromAccountId, int toAccountId, decimal amount)
        {
            Account fromAccount = _unitOfWork.AccountRepository.GetById(fromAccountId)!;
            if (fromAccount == null)
                throw new ArgumentNullException($"Account with Id {fromAccountId} not found");

            Account toAccount = _unitOfWork.AccountRepository.GetById(toAccountId)!;
            if (toAccount == null)
                throw new ArgumentNullException($"Account with Id {toAccountId} not found");

            if (fromAccount.Status != AccountStatus.Active || toAccount.Status != AccountStatus.Active)
                throw new InvalidOperationException("Both of account must be active to transfer");

            if (fromAccountId == toAccountId)
                throw new InvalidOperationException("Cannot transfer to same account");

            if (fromAccount.Balance <= amount)
                throw new InvalidOperationException("There is no enough amount to transfer");

            fromAccount.Balance -= amount;
            toAccount.Balance += amount;

            Transaction transaction = new()
            {
                FromAccountId = fromAccountId,
                ToAccountId = toAccountId,
                Amount = amount,
                Description = $"Transfer from {fromAccount.AccountNumber} to {toAccount.AccountNumber}",
                TransactionDate = DateTime.UtcNow
            };

            _unitOfWork.TransactionRepository.Insert(transaction);
            _unitOfWork.AccountRepository.Update(fromAccount);
            _unitOfWork.AccountRepository.Update(toAccount);
            _unitOfWork.SaveChanges();
            OnTransactionProcessed(transaction);
        }

        public async Task TransferMoneyAsync(int fromAccountId, int toAccountId, decimal amount, CancellationToken token)
        {
            Account fromAccount = await _unitOfWork.AccountRepository.GetByIdAsync(fromAccountId, token)!;
            if (fromAccount == null)
                throw new ArgumentNullException($"Account with Id {fromAccountId} not found");

            Account toAccount = await _unitOfWork.AccountRepository.GetByIdAsync(toAccountId, token)!;
            if (toAccount == null)
                throw new ArgumentNullException($"Account with Id {toAccountId} not found");

            if (fromAccount.Status != AccountStatus.Active || toAccount.Status != AccountStatus.Active)
                throw new InvalidOperationException("Both of account must be active to transfer");

            if (fromAccountId == toAccountId)
                throw new InvalidOperationException("Cannot transfer to same account");

            if (fromAccount.Balance <= amount)
                throw new InvalidOperationException("There is no enough amount to transfer");

            fromAccount.Balance -= amount;
            toAccount.Balance += amount;

            Transaction transaction = new()
            {
                FromAccountId = fromAccountId,
                ToAccountId = toAccountId,
                Amount = amount,
                Description = $"Transfer from {fromAccount.AccountNumber} to {toAccount.AccountNumber}",
                TransactionDate = DateTime.UtcNow
            };

            await _unitOfWork.TransactionRepository.InsertAsync(transaction, token);
            await _unitOfWork.AccountRepository.UpdateAsync(fromAccount, token);
            await _unitOfWork.AccountRepository.UpdateAsync(toAccount, token);
            await _unitOfWork.SavechangesAsync(token);
            OnTransactionProcessed(transaction);
        }

        public void Withdraw(int fromAccountId, decimal amount)
        {
            Account account = _unitOfWork.AccountRepository.GetById(fromAccountId)!;

            if (account == null)
                throw new KeyNotFoundException($"Account with Id {fromAccountId} not found");

            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.", nameof(amount));

            if (account.Balance < amount)
                throw new InvalidOperationException("There is no enough amount on your account to withdraw");

            account.Balance -= amount;

            Transaction transaction = new()
            {
                FromAccountId = fromAccountId,
                Amount = amount,
                Description = $"WithDrawn {amount} from account {account.AccountNumber}",
                TransactionDate = DateTime.UtcNow
            };

            _unitOfWork.TransactionRepository.Insert(transaction);
            _unitOfWork.AccountRepository.Update(account);
            _unitOfWork.SaveChanges();
            OnTransactionProcessed(transaction);
        }

        public async Task WithdrawAsync(int fromAccountId, decimal amount, CancellationToken token)
        {
            Account account = await _unitOfWork.AccountRepository.GetByIdAsync(fromAccountId, token)!;

            if (account == null)
                throw new KeyNotFoundException($"Account with Id {fromAccountId} not found");

            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.", nameof(amount));

            if (account.Balance < amount)
                throw new InvalidOperationException("There is no enough amount on your account to withdraw");

            account.Balance -= amount;

            Transaction transaction = new()
            {
                FromAccountId = fromAccountId,
                Amount = amount,
                Description = $"WithDrawn {amount} from account {account.AccountNumber}",
                TransactionDate = DateTime.UtcNow
            };

            await _unitOfWork.TransactionRepository.InsertAsync(transaction, token);
            await _unitOfWork.AccountRepository.UpdateAsync(account, token);
            await _unitOfWork.SavechangesAsync(token);
            OnTransactionProcessed(transaction);
        }

        private void OnTransactionProcessed(Transaction transaction)
        {
            TransactionProcessed?.Invoke(transaction);
        }
    }
}
