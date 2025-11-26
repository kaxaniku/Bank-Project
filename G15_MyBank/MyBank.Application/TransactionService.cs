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

    public static event Action<Transaction>? TransactionMade;

    public void TransferMoney(int fromAccountId, int toAccountId, decimal amount)
    {
        Account fromAccount = _unitOfWork.AccountRepository.GetById(fromAccountId)
            ?? throw new InvalidOperationException($"Account with ID {fromAccountId} does not exist.");
        Account toAccount = _unitOfWork.AccountRepository.GetById(toAccountId)
            ?? throw new InvalidOperationException($"Account with ID {toAccountId} does not exist.");
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
        OnTransactionMade(transaction);
    }

    public void ProcessCardPayment(int cardId, int recieverId, decimal amount)
    {
        Card card = _unitOfWork.CardRepository.GetById(cardId)
            ?? throw new InvalidOperationException($"Card with ID {cardId} does not exist.");
        Account reciever = _unitOfWork.AccountRepository.GetById(recieverId)
            ?? throw new InvalidOperationException($"Account with ID {recieverId} does not exist.");
        Account account = card.Account;
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
        OnTransactionMade(transaction);
    }

    public bool IsTransactionAllowed(int fromAccountId, int toAccountId, decimal amount)
    {
        Account fromAccount = _unitOfWork.AccountRepository.GetById(fromAccountId)
            ?? throw new InvalidOperationException($"Account with ID {fromAccountId} does not exist.");
        Account toAccount = _unitOfWork.AccountRepository.GetById(toAccountId)
            ?? throw new InvalidOperationException($"Account with ID {toAccountId} does not exist.");

        if (fromAccountId == toAccountId)
        {
            return false;
        }

        if(amount <= 0)
        {
            return false;
        }

        if(fromAccount.Status != AccountStatus.Active || toAccount.Status != AccountStatus.Active)
        {
            return false;
        }

        return fromAccount.Balance >= amount;
    }

    public Transaction? GetTransaction(int transactionId)
    {
        return _unitOfWork.TransactionRepository.GetById(transactionId);
    }

    public IEnumerable<Transaction> ListTransactions(int accountId)
    {
        return _unitOfWork.TransactionRepository.Query(t => true);
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
        Account fromAccount = await _unitOfWork.AccountRepository.GetByIdAsync(fromAccountId, cancellationToken)
            ?? throw new InvalidOperationException($"Account with ID {fromAccountId} does not exist.");
        Account toAccount = await _unitOfWork.AccountRepository.GetByIdAsync(toAccountId, cancellationToken)
            ?? throw new InvalidOperationException($"Account with ID {toAccountId} does not exist.");
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

        await _unitOfWork.TransactionRepository.InsertAsync(transaction, cancellationToken);
        await _unitOfWork.AccountRepository.UpdateAsync(fromAccount);
        await _unitOfWork.AccountRepository.UpdateAsync(toAccount);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnTransactionMade(transaction);
    }

    public async Task ProcessCardPaymentAsync(int cardId, int recieverId, decimal amount, CancellationToken cancellationToken)
    {
        Card card = await _unitOfWork.CardRepository.GetByIdAsync(cardId, cancellationToken)
            ?? throw new InvalidOperationException($"Card with ID {cardId} does not exist.");
        Account reciever = await _unitOfWork.AccountRepository.GetByIdAsync(recieverId, cancellationToken)
            ?? throw new InvalidOperationException($"Account with ID {recieverId} does not exist.");
        Account account = card.Account;
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

        await _unitOfWork.TransactionRepository.InsertAsync(transaction, cancellationToken);
        await _unitOfWork.AccountRepository.UpdateAsync(account);
        await _unitOfWork.AccountRepository.UpdateAsync(reciever);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        OnTransactionMade(transaction);
    }

    public async Task<bool> IsTransactionAllowedAsync(int fromAccountId, int toAccountId, decimal amount, CancellationToken cancellationToken)
    {
        Account fromAccount = await _unitOfWork.AccountRepository.GetByIdAsync(fromAccountId, cancellationToken)
            ?? throw new InvalidOperationException($"Account with ID {fromAccountId} does not exist.");
        Account toAccount = await _unitOfWork.AccountRepository.GetByIdAsync(toAccountId, cancellationToken)
            ?? throw new InvalidOperationException($"Account with ID {toAccountId} does not exist.");

        if (fromAccountId == toAccountId)
        {
            return false;
        }

        if (amount <= 0)
        {
            return false;
        }

        if (fromAccount.Status != AccountStatus.Active || toAccount.Status != AccountStatus.Active)
        {
            return false;
        }

        return fromAccount.Balance >= amount;
    }

    public async Task<Transaction?> GetTransactionAsync(int transactionId, CancellationToken cancellationToken)
    {
        return await _unitOfWork.TransactionRepository.GetByIdAsync(transactionId, cancellationToken);
    }

    public async Task<IEnumerable<Transaction>> ListTransactionsAsync(int accountId, CancellationToken cancellationToken)
    {
        return await _unitOfWork.TransactionRepository.QueryAsync(t => true, cancellationToken);
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
