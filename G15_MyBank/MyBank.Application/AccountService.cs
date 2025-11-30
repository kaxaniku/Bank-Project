using MyBank.Application.Interfaces.Repositories;
using MyBank.Application.Interfaces.Services;
using MyBank.Domain;

namespace MyBank.Application
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public event Action<Account>? AccountOpened;
        public event Action<Account>? AccountUpdated;
        public event Action<int>? AccountClosed;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        } 
        
        public IEnumerable<Account> GetAccountsByCostumer(int customerId)
        {
            return _unitOfWork.AccountRepository.Query(c => c.Customer.CustomerId == customerId).ToList();
        }

        public async Task<IEnumerable<Account>> GetAccountsByCustomerAsync(int customerId, CancellationToken token)
        {
            return await _unitOfWork.AccountRepository.QueryAsync(c => c.Customer.CustomerId == customerId, token);
        }
        
        public void OpenNewAccount(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account), "Account cannot be null.");

            _unitOfWork.AccountRepository.Insert(account);
            _unitOfWork.SaveChanges();
            OnAccountOpened(account);
        } 
        
        public async Task OpenNewAccountAsync(Account account, CancellationToken token)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account), "Account cannot be null.");

            await _unitOfWork.AccountRepository.InsertAsync(account, token);
            await _unitOfWork.SavechangesAsync(token);
            OnAccountOpened(account);
        }
        
        public decimal CheckBalance(int accountId)
        {
            Account account = _unitOfWork.AccountRepository.GetById(accountId)!;
            if (account == null)
                throw new KeyNotFoundException($"Account with Id {accountId} was not found.");

            return account.Balance;
        }

        public async Task<decimal> CheckBalanceAsync(int accountId, CancellationToken token)
        {
            Account account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId, token);
            if (account == null)
                throw new KeyNotFoundException($"Account with Id {accountId} was not found.");

            return account.Balance;
        }

        public void FreezeAccount(int accountId)
        {
            Account account = _unitOfWork.AccountRepository.GetById(accountId)!;

            if(account == null)
                throw new KeyNotFoundException($"Account with id {accountId} not found");

            if (account.Status == AccountStatus.Closed)
                throw new InvalidOperationException("Closed account cannot be frozen.");

            if (account.Status == AccountStatus.Frozen)
                throw new InvalidOperationException("Account is already frozen.");

            account.Status = AccountStatus.Frozen;
            _unitOfWork.AccountRepository.Update(account);
            _unitOfWork.SaveChanges();
            OnAccountUpdated(account);
        }

        public async Task FreezeAccountAsync(int accountId, CancellationToken token)
        {
            Account account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId, token);

            if (account == null)
                throw new KeyNotFoundException($"Account with id {accountId} not found");

            if (account.Status == AccountStatus.Closed)
                throw new InvalidOperationException("Closed account cannot be frozen.");

            if (account.Status == AccountStatus.Frozen)
                throw new InvalidOperationException("Account is already frozen.");

            account.Status = AccountStatus.Frozen;

            await _unitOfWork.AccountRepository.UpdateAsync(account, token);
            await _unitOfWork.SavechangesAsync(token);
            OnAccountUpdated(account);
        }

        public void UnfreezeAccount(int accountId)
        {
            Account account = _unitOfWork.AccountRepository.GetById(accountId)!;

            if (account == null)
                throw new KeyNotFoundException($"Account with id {accountId} not found");

            if (account.Status == AccountStatus.Active)
                throw new InvalidOperationException("Account is already activated.");

            account.Status = AccountStatus.Active;

            _unitOfWork.AccountRepository.Update(account);
            _unitOfWork.SaveChanges();
            OnAccountUpdated(account);
        }

        public async Task UnfreezeAccountAsync(int accountId, CancellationToken token)
        {
            Account account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId, token);

            if (account == null)
                throw new KeyNotFoundException($"Account with id {accountId} not found");

            if (account.Status == AccountStatus.Active)
                throw new InvalidOperationException("Account is already activated.");

            account.Status = AccountStatus.Active;

            await _unitOfWork.AccountRepository.UpdateAsync(account, token);
            await _unitOfWork.SavechangesAsync(token);
            OnAccountUpdated(account);
        }
        
        public void CloseAccount(int accountId)
        {
            Account account = _unitOfWork.AccountRepository.GetById(accountId)!;
            if (account == null)
                throw new KeyNotFoundException($"Account with Id {accountId} was not found.");

            _unitOfWork.AccountRepository.Delete(account);
            _unitOfWork.SaveChanges();
            OnAccountClosed(accountId);
        }

        public async Task CloseAccountAsync(int accountId, CancellationToken token)
        {
            Account account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId, token);
            if (account == null)
                throw new KeyNotFoundException($"Account with Id {accountId} was not found.");

            await _unitOfWork.AccountRepository.DeleteAsync(account, token);
            await _unitOfWork.SavechangesAsync(token);
            OnAccountClosed(accountId);
        }

        private void OnAccountOpened(Account account)
        {
            AccountOpened?.Invoke(account);
        }

        private void OnAccountUpdated(Account account)
        {
            AccountUpdated?.Invoke(account);
        }

        private void OnAccountClosed(int accountId)
        {
            AccountClosed?.Invoke(accountId);
        }
    }
}
