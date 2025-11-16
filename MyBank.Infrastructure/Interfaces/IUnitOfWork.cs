using MyBank.Infrastructure.Interfaces;

namespace MyBank.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        ITransactionRepository Transactions { get; }
        Task<int> SaveChangesAsync();
    }
}
