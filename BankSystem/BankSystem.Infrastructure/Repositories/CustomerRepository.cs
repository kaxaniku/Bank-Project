using BankSystem.Application.Common.Interfaces.Repositories;
using BankSystem.Domain.Entities;
using BankSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Infrastructure.Repositories;

public class CustomerRepository(BankSystemDbContext context) : RepositoryBase<Customer>(context), ICustomerRepository
{
    public async Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(c => c.Email == email, cancellationToken);
    }

    public async Task<Customer?> GetByEmailOrNationalIdAsync(string email, string nationalId, CancellationToken cancellationToken)
    {
        return await DbSet.FirstOrDefaultAsync(c => c.NationalId == nationalId || c.Email == email, cancellationToken);
    }

    public async Task<Customer?> GetByNationalIdAsync(string nationalId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(c => c.Accounts)
            .FirstOrDefaultAsync(c => c.NationalId == nationalId, cancellationToken);
    }

    public async Task<Customer?> GetWithAccountsAsync(int customerId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(c => c.Accounts)
            .FirstOrDefaultAsync(c => c.Id == customerId, cancellationToken);
    }
}