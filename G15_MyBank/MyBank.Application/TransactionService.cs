using System.Security.Principal;
using System.Threading;
using MyBank.Application.Interfaces.Repositories;
using MyBank.Application.Interfaces.Services;
using MyBank.Domain;

namespace MyBank.Application;

public sealed class TransactionService : ITransactionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccountService _accountService;
    private readonly ICardService _cardService;

    public TransactionService(IUnitOfWork unitOfWork, IAccountService accountService, ICardService cardService)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        _cardService = cardService;
    }

    public static event Action<Transaction>? TransactionMade;

    public void TransferMoney(int fromAccountId, int toAccountId, decimal amount)
    {
        Account fromAccount = _accountService.FindAccount(fromAccountId);
        Account toAccount = _accountService.FindAccount(toAccountId);

        if(fromAccountId == toAccountId)
        {
            throw new InvalidOperationException("Cannot transfer money to the same account.");
        }
        if(amount <= 0)
        {
            throw new InvalidOperationException("Transfer amount must be greater than zero.");
        }
        if(fromAccount.Status != AccountStatus.Active || toAccount.Status != AccountStatus.Active)
        {
            throw new InvalidOperationException("Both accounts must be active to perform a transfer.");
        }
        if(fromAccount.Balance < amount)
        {
            throw new InvalidOperationException("Insufficient funds in the source account.");
        }
        fromAccount.Balance -= amount;
        toAccount.Balance += amount;

        Transaction transaction = new()
        {
            FromAccountId = fromAccountId,
            ToAccountId = toAccountId,
            Amount = amount,
            Description = $"Transfer from {fromAccount.AccountNumber} to {toAccount.AccountNumber}",
            Type = TransactionType.Transfer,
            TransactionDate = DateTime.UtcNow
        };

        try
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.TransactionRepository.Insert(transaction);
            _unitOfWork.AccountRepository.Update(fromAccount);
            _unitOfWork.AccountRepository.Update(toAccount);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }

        OnTransactionMade(transaction);
    }

    public void DepositMoney(int toAccountId, decimal amount)
    {
        Account toAccount = _accountService.FindAccount(toAccountId);
        if (amount <= 0)
        {
            throw new InvalidOperationException("Deposit amount must be greater than zero.");
        }
        if (toAccount.Status != AccountStatus.Active)
        {
            throw new InvalidOperationException("Account must be active to perform a deposition.");
        }
        toAccount.Balance += amount;

        Transaction transaction = new()
        {
            ToAccountId = toAccountId,
            Amount = amount,
            Description = $"Deposited money to {toAccount.AccountNumber}",
            Type = TransactionType.Deposit,
            TransactionDate = DateTime.UtcNow
        };

        try
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.TransactionRepository.Insert(transaction);
            _unitOfWork.AccountRepository.Update(toAccount);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }

        OnTransactionMade(transaction);
    }

    public void WithdrawMoney(int fromAccountId, decimal amount)
    {
        Account fromAccount = _accountService.FindAccount(fromAccountId);
        if (amount <= 0)
        {
            throw new InvalidOperationException("Withdrawl amount must be greater than zero.");
        }
        if (fromAccount.Status != AccountStatus.Active)
        {
            throw new InvalidOperationException("Account must be active to perform a deposition.");
        }
        if (fromAccount.Balance < amount)
        {
            throw new InvalidOperationException("Insufficient funds in the source account.");
        }
        fromAccount.Balance -= amount;

        Transaction transaction = new()
        {
            FromAccountId = fromAccountId,
            Amount = amount,
            Description = $"Withdrawn Money from {fromAccount.AccountNumber}",
            Type = TransactionType.Withdrawal,
            TransactionDate = DateTime.UtcNow
        };

        try
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.TransactionRepository.Insert(transaction);
            _unitOfWork.AccountRepository.Update(fromAccount);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }

        OnTransactionMade(transaction);
    }

    public void ProcessCardPayment(int cardId, int recieverId, decimal amount)
    {
        Card card = _cardService.FindCard(cardId);
        Account reciever = _accountService.FindAccount(recieverId);
        Account account = card.Account;
        if (account.AccountId == recieverId)
        {
            throw new InvalidOperationException("Cannot transfer money to the same account.");
        }
        if (amount <= 0)
        {
            throw new InvalidOperationException("Transfer amount must be greater than zero.");
        }
        if (account.Status != AccountStatus.Active || reciever.Status != AccountStatus.Active)
        {
            throw new InvalidOperationException("Both accounts must be active to perform a transfer.");
        }
        if (account.Balance < amount)
        {
            throw new InvalidOperationException("Insufficient funds in the source account.");
        }
        if (card.Status != CardStatus.Active)
        {
            throw new InvalidOperationException("Card must be active to process the payment");
        }
        account.Balance -= amount;
        reciever.Balance += amount;

        Transaction transaction = new()
        {
            FromAccountId = account.AccountId,
            ToAccountId = recieverId,
            Amount = amount,
            Description = $"Card payment from card {card.CardNumber}",
            Type = TransactionType.CardPayment,
            TransactionDate = DateTime.UtcNow
        };

        try
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.TransactionRepository.Insert(transaction);
            _unitOfWork.AccountRepository.Update(account);
            _unitOfWork.AccountRepository.Update(reciever);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }

        OnTransactionMade(transaction);
    }

    public Transaction? GetTransaction(int transactionId)
    {
        return _unitOfWork.TransactionRepository.GetById(transactionId);
    }

    public IEnumerable<Transaction> ListTransactions(int accountId, TransactionType type)
    {
        return _unitOfWork.TransactionRepository.Query(x => x.Type == type);
    }

    public IEnumerable<Transaction> GenerateStatement(int accountId, DateTime fromDate, DateTime toDate)
    {
        return _unitOfWork.TransactionRepository.Query(x => 
            (x.FromAccountId == accountId || x.ToAccountId == accountId) &&
            x.TransactionDate >= fromDate &&
            x.TransactionDate <= toDate);
    }

    public async Task TransferMoneyAsync(int fromAccountId, int toAccountId, decimal amount, CancellationToken cancellationToken)
    {
        Account fromAccount = await _accountService.FindAccountAsync(fromAccountId, cancellationToken);
        Account toAccount = await _accountService.FindAccountAsync(toAccountId, cancellationToken);
        if (fromAccountId == toAccountId)
        {
            throw new InvalidOperationException("Cannot transfer money to the same account.");
        }
        if (amount <= 0)
        {
            throw new InvalidOperationException("Transfer amount must be greater than zero.");
        }
        if (fromAccount.Status != AccountStatus.Active || toAccount.Status != AccountStatus.Active)
        {
            throw new InvalidOperationException("Both accounts must be active to perform a transfer.");
        }
        if (fromAccount.Balance < amount)
        {
            throw new InvalidOperationException("Insufficient funds in the source account.");
        }
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

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _unitOfWork.TransactionRepository.InsertAsync(transaction, cancellationToken);
            await _unitOfWork.AccountRepository.UpdateAsync(fromAccount);
            await _unitOfWork.AccountRepository.UpdateAsync(toAccount);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
        catch
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }

        OnTransactionMade(transaction);
    }

    public async Task DepositMoneyAsync(int toAccountId, decimal amount, CancellationToken cancellationToken)
    {
        Account toAccount = await _accountService.FindAccountAsync(toAccountId, cancellationToken);
        if (amount <= 0)
        {
            throw new InvalidOperationException("Deposit amount must be greater than zero.");
        }
        if (toAccount.Status != AccountStatus.Active)
        {
            throw new InvalidOperationException("Account must be active to perform a deposition.");
        }
        toAccount.Balance += amount;

        Transaction transaction = new()
        {
            ToAccountId = toAccountId,
            Amount = amount,
            Description = $"Deposited money to {toAccount.AccountNumber}",
            Type = TransactionType.Deposit,
            TransactionDate = DateTime.UtcNow
        };

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _unitOfWork.TransactionRepository.InsertAsync(transaction, cancellationToken);
            await _unitOfWork.AccountRepository.UpdateAsync(toAccount);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
        catch
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }

        OnTransactionMade(transaction);
    }

    public async Task WithdrawMoneyAsync(int fromAccountId, decimal amount, CancellationToken cancellationToken)
    {
        Account fromAccount = await _accountService.FindAccountAsync(fromAccountId, cancellationToken);
        if (amount <= 0)
        {
            throw new InvalidOperationException("Withdrawl amount must be greater than zero.");
        }
        if (fromAccount.Status != AccountStatus.Active)
        {
            throw new InvalidOperationException("Account must be active to perform a deposition.");
        }
        if (fromAccount.Balance < amount)
        {
            throw new InvalidOperationException("Insufficient funds in the source account.");
        }
        fromAccount.Balance -= amount;

        Transaction transaction = new()
        {
            FromAccountId = fromAccountId,
            Amount = amount,
            Description = $"Withdrawn Money from {fromAccount.AccountNumber}",
            Type = TransactionType.Withdrawal,
            TransactionDate = DateTime.UtcNow
        };

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _unitOfWork.TransactionRepository.InsertAsync(transaction, cancellationToken);
            await _unitOfWork.AccountRepository.UpdateAsync(fromAccount);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
        catch
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }

        OnTransactionMade(transaction);
    }

    public async Task ProcessCardPaymentAsync(int cardId, int recieverId, decimal amount, CancellationToken cancellationToken)
    {
        Card card = await _cardService.FindCardAsync(cardId, cancellationToken);
        Account reciever = await _accountService.FindAccountAsync(recieverId, cancellationToken);
        Account account = card.Account;
        if (account.AccountId == recieverId)
        {
            throw new InvalidOperationException("Cannot transfer money to the same account.");
        }
        if (amount <= 0)
        {
            throw new InvalidOperationException("Transfer amount must be greater than zero.");
        }
        if (account.Status != AccountStatus.Active || reciever.Status != AccountStatus.Active)
        {
            throw new InvalidOperationException("Both accounts must be active to perform a transfer.");
        }
        if (account.Balance < amount)
        {
            throw new InvalidOperationException("Insufficient funds in the source account.");
        }
        if (card.Status != CardStatus.Active)
        {
            throw new InvalidOperationException("Card must be active to process the payment");
        }
        account.Balance -= amount;
        reciever.Balance += amount;

        Transaction transaction = new()
        {
            FromAccountId = account.AccountId,
            ToAccountId = recieverId,
            Amount = amount,
            Description = $"Card payment from card {card.CardNumber}",
            Type = TransactionType.CardPayment,
            TransactionDate = DateTime.UtcNow
        };

        try 
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _unitOfWork.TransactionRepository.InsertAsync(transaction, cancellationToken);
            await _unitOfWork.AccountRepository.UpdateAsync(account);
            await _unitOfWork.AccountRepository.UpdateAsync(reciever);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
        catch
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }

        OnTransactionMade(transaction);
    }

    public async Task<Transaction?> GetTransactionAsync(int transactionId, CancellationToken cancellationToken)
    {
        return await _unitOfWork.TransactionRepository.GetByIdAsync(transactionId, cancellationToken);
    }

    public async Task<IEnumerable<Transaction>> ListTransactionsAsync(int accountId, TransactionType type, CancellationToken cancellationToken)
    {
        return await _unitOfWork.TransactionRepository.QueryAsync(x => x.Type == type, cancellationToken);
    }

    public async Task<IEnumerable<Transaction>> GenerateStatementAsync(int accountId, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken)
    {
        return await _unitOfWork.TransactionRepository.QueryAsync(x =>
            (x.FromAccountId == accountId || x.ToAccountId == accountId) &&
            x.TransactionDate >= fromDate &&
            x.TransactionDate <= toDate, cancellationToken);
    }

    private static void OnTransactionMade(Transaction transaction)
    {
        TransactionMade?.Invoke(transaction);
    }
}
