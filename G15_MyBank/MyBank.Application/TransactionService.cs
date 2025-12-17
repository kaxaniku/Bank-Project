using MyBank.Application.Interfaces.Repositories;
using MyBank.Application.Interfaces.Services;
using MyBank.Domain;
using Microsoft.EntityFrameworkCore;

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

    public void TransferMoney(string fromAccountNum, string toAccountNum, decimal amount)
    {
        Account fromAccount = _accountService.FindAccount(fromAccountNum);
        Account toAccount = _accountService.FindAccount(toAccountNum);

        if (fromAccountNum == toAccountNum)
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
            FromAccountId = fromAccount.AccountId,
            ToAccountId = toAccount.AccountId,
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

    public void DepositMoney(string toAccountNum, decimal amount)
    {
        Account toAccount = _accountService.FindAccount(toAccountNum);
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
            ToAccountId = toAccount.AccountId,
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

    public void WithdrawMoney(string fromAccountNum, decimal amount)
    {
        Account fromAccount = _accountService.FindAccount(fromAccountNum);
        if (amount <= 0)
        {
            throw new InvalidOperationException("Withdrawal amount must be greater than zero.");
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
            FromAccountId = fromAccount.AccountId,
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

    public void ProcessCardPayment(string cardNum, string recieverNum, decimal amount)
    {
        Card card = _cardService.FindCard(cardNum);
        Account reciever = _accountService.FindAccount(recieverNum);
        Account account = card.Account;
        if (account.AccountId == reciever.AccountId)
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
            ToAccountId = reciever.AccountId,
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

    public IEnumerable<Transaction> ListTransactions(TransactionType type, int pageNumber = 1)
    {
        if (pageNumber < 1)
            throw new ArgumentException("Page number must be >= 1.", nameof(pageNumber));

        const int pageSize = 10;
        return _unitOfWork.TransactionRepository.ListByType(type, pageNumber, pageSize);
    }


    public IEnumerable<Transaction> GenerateStatement(string accountNum, DateTime fromDate, DateTime toDate, int pageNumber = 1)
    {
        if (pageNumber < 1)
            throw new ArgumentException("Page number must be >= 1.", nameof(pageNumber));

        Account account = _accountService.FindAccount(accountNum);

        const int pageSize = 10;
        return _unitOfWork.TransactionRepository.ListForAccountBetweenDates(
            account.AccountId, fromDate, toDate, pageNumber, pageSize);
    }


    public async Task TransferMoneyAsync(string fromAccountNum, string toAccountNum, decimal amount, CancellationToken cancellationToken)
    {
        Account fromAccount = await _accountService.FindAccountAsync(fromAccountNum, cancellationToken);
        Account toAccount = await _accountService.FindAccountAsync(toAccountNum, cancellationToken);
        if (fromAccountNum == toAccountNum)
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
            FromAccountId = fromAccount.AccountId,
            ToAccountId = toAccount.AccountId,
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

    public async Task DepositMoneyAsync(string toAccountNum, decimal amount, CancellationToken cancellationToken)
    {
        Account toAccount = await _accountService.FindAccountAsync(toAccountNum, cancellationToken);
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
            ToAccountId = toAccount.AccountId,
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

    public async Task WithdrawMoneyAsync(string fromAccountNum, decimal amount, CancellationToken cancellationToken)
    {
        Account fromAccount = await _accountService.FindAccountAsync(fromAccountNum, cancellationToken);
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
            FromAccountId = fromAccount.AccountId,
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

    public async Task ProcessCardPaymentAsync(string cardNum, string recieverNum, decimal amount, CancellationToken cancellationToken)
    {
        Card card = await _cardService.FindCardAsync(cardNum, cancellationToken);
        Account reciever = await _accountService.FindAccountAsync(recieverNum, cancellationToken);
        Account account = card.Account;
        if (account.AccountId == reciever.AccountId)
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
            ToAccountId = reciever.AccountId,
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

    public async Task<IEnumerable<Transaction>> ListTransactionsAsync(TransactionType type, CancellationToken cancellationToken, int pageNumber = 1)
    {
        if (pageNumber < 1)
            throw new ArgumentException("Page number must be >= 1.", nameof(pageNumber));

        const int pageSize = 10;
        int skip = (pageNumber - 1) * pageSize;

        IQueryable<Transaction> query =
            _unitOfWork.TransactionRepository
                .Query(x => x.Type == type);

        return await query
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Transaction>> GenerateStatementAsync(string accountNum, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken, int pageNumber = 1)
    {
        if (pageNumber < 1)
            throw new ArgumentException("Page number must be >= 1.", nameof(pageNumber));

        Account account = _accountService.FindAccount(accountNum);

        const int pageSize = 10;
        int skip = (pageNumber - 1) * pageSize;

        IQueryable<Transaction> query =
            _unitOfWork.TransactionRepository.Query(x =>
            (x.FromAccountId == account.AccountId || x.ToAccountId == account.AccountId) &&
            x.TransactionDate >= fromDate &&
            x.TransactionDate <= toDate);

        return await query
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();
    }

    private static void OnTransactionMade(Transaction transaction)
    {
        TransactionMade?.Invoke(transaction);
    }
}
