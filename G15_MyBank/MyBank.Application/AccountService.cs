using MyBank.Application.Interfaces.Repositories;
using MyBank.Application.Interfaces.Services;
using MyBank.Domain;

namespace MyBank.Application
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public decimal CheckBalance(int accountId)
        {
            var account = _unitOfWork.AccountRepository.GetById(accountId);
            if (account == null)
                throw new KeyNotFoundException($"Account with Id {accountId} was not found.");

            return account.Balance;
        }

        public async Task<decimal> CheckBalanceAsync(int accountId, CancellationToken token)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId, token);
            if (account == null)
                throw new KeyNotFoundException($"Account with Id {accountId} was not found.");

            return account.Balance;
        }

        public void CloseAccount(int accountId)
        {
            var account = _unitOfWork.AccountRepository.GetById(accountId);
            if (account == null)
                throw new KeyNotFoundException($"Account with Id {accountId} was not found.");

            _unitOfWork.AccountRepository.Delete(account);
            _unitOfWork.SaveChanges();
        }

        public async Task CloseAccountAsync(int accountId, CancellationToken token)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId, token);
            if (account == null)
                throw new KeyNotFoundException($"Account with Id {accountId} was not found.");

            await _unitOfWork.AccountRepository.DeleteAsync(account, token);
            await _unitOfWork.SavechangesAsync(token);
        }

        public void Deposit(int accountId, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentNullException("Amount must be positive to deposit");

            var account = _unitOfWork.AccountRepository.GetById(accountId);
            account!.Balance += amount;

            _unitOfWork.AccountRepository.Update(account);
            _unitOfWork.SaveChanges();
        }

        public async Task DepositAsync(int accountId, decimal amount, CancellationToken token)
        {
            if (amount <= 0)
                throw new ArgumentNullException("Amount must be positive to deposit");

            var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId, token);
            account.Balance += amount;

            await _unitOfWork.AccountRepository.UpdateAsync(account, token);
            await _unitOfWork.SavechangesAsync(token);
        }

        public void FreezeAccount(int accountId)
        {
            var account = _unitOfWork.AccountRepository.GetById(accountId);

            if(account == null)
                throw new KeyNotFoundException($"Account with id {accountId} not found");

            if (account.Status == AccountStatus.Closed)
                throw new InvalidOperationException("Closed account cannot be frozen.");

            if (account.Status == AccountStatus.Frozen)
                throw new InvalidOperationException("Account is already frozen.");

            account.Status = AccountStatus.Frozen;
            _unitOfWork.AccountRepository.Update(account);
            _unitOfWork.SaveChanges();
        }

        public async Task FreezeAccountAsync(int accountId, CancellationToken token)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId, token);

            if (account == null)
                throw new KeyNotFoundException($"Account with id {accountId} not found");

            if (account.Status == AccountStatus.Closed)
                throw new InvalidOperationException("Closed account cannot be frozen.");

            if (account.Status == AccountStatus.Frozen)
                throw new InvalidOperationException("Account is already frozen.");

            account.Status = AccountStatus.Frozen;
            await _unitOfWork.AccountRepository.UpdateAsync(account, token);
            await _unitOfWork.SavechangesAsync(token);
        }

        public IEnumerable<Account> GetAccountsByCostumer(int customerId)
        {
            var accounts = _unitOfWork.AccountRepository.Query(c => c.Customer.CustomerId == customerId).ToList();
            return accounts;
        }

        public async Task<IEnumerable<Account>> GetAccountsByCustomerAsync(int customerId, CancellationToken token)
        {
            var accounts = await _unitOfWork.AccountRepository.QueryAsync(c => c.Customer.CustomerId == customerId, token);
            return accounts;
        }

        public void OpenNewAccount(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account), "Account cannot be null.");

            _unitOfWork.AccountRepository.Insert(account);
            _unitOfWork.SaveChanges();
        }

        public async Task OpenNewAccountAsync(Account account, CancellationToken token)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account), "Account cannot be null.");

            await _unitOfWork.AccountRepository.InsertAsync(account, token);
            await _unitOfWork.SavechangesAsync(token);
        }

        public void UnfreezeAccount(int accountId)
        {
            var account = _unitOfWork.AccountRepository.GetById(accountId);

            if (account == null)
                throw new KeyNotFoundException($"Account with id {accountId} not found");

            if (account.Status == AccountStatus.Active)
                throw new InvalidOperationException("Account is already activated.");

            account.Status = AccountStatus.Active;
            _unitOfWork.AccountRepository.Update(account);
            _unitOfWork.SaveChanges();
        }

        public async Task UnfreezeAccountAsync(int accountId, CancellationToken token)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId, token);

            if (account == null)
                throw new KeyNotFoundException($"Account with id {accountId} not found");

            if (account.Status == AccountStatus.Active)
                throw new InvalidOperationException("Account is already activated.");

            account.Status = AccountStatus.Active;
            await _unitOfWork.AccountRepository.UpdateAsync(account, token);
            await _unitOfWork.SavechangesAsync(token);
        }

        public void Withdraw(int accountId, decimal amount)
        {
            var account = _unitOfWork.AccountRepository.GetById(accountId);

            if (account == null)
                throw new KeyNotFoundException($"Account with Id {accountId} not found");

            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.", nameof(amount));

            if (account.Balance < amount)
                throw new InvalidOperationException("There is no enough amount on your account to withdraw");

            account.Balance -= amount;
            _unitOfWork.SaveChanges();
        }

        public async Task WithdrawAsync(int accountId, decimal amount, CancellationToken token)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId, token);

            if (account == null)
                throw new KeyNotFoundException($"Account with Id {accountId} not found");

            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.", nameof(amount));

            if (account.Balance < amount)
                throw new InvalidOperationException("There is no enough amount on your account to withdraw");

            account.Balance -= amount;
            await _unitOfWork.SavechangesAsync(token);
        }
    }
}
