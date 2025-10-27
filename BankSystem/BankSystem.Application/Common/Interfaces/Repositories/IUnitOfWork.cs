using Microsoft.EntityFrameworkCore.Storage;

namespace BankSystem.Application.Common.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    ICustomerRepository Customers { get; }
    IAccountRepository Accounts { get; }
    ICardRepository Cards { get; }
    ITransactionRepository Transactions { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}