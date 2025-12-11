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
        public event Action<Account>? AccountClosed;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public IEnumerable<Account> GetAccountsByCostumer(string personalNumber)
        {
            return _unitOfWork.AccountRepository.Query(c => c.Customer.PersonalNumber.Equals(personalNumber)).ToList();
        }

        public async Task<IEnumerable<Account>> GetAccountsByCustomerAsync(string personaNumber, CancellationToken token)
        {
            var accounts = await _unitOfWork.AccountRepository.QueryAsync(c => c.Customer.PersonalNumber.Equals(personaNumber), token);

            return accounts.ToList();
        }

        public void OpenNewAccount(string personalNumber, string accountNumber, decimal balance)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(personalNumber);
            ArgumentException.ThrowIfNullOrWhiteSpace(accountNumber);
            ArgumentOutOfRangeException.ThrowIfNegative(balance);

            Customer? customer = _unitOfWork.CustomerRepository.Query(c => c.PersonalNumber.Equals(personalNumber)).FirstOrDefault();

            ArgumentNullException.ThrowIfNull(customer);

            Account account = new()
            {
                Customer = customer,
                AccountNumber = accountNumber,
                Balance = balance
            };

            ArgumentNullException.ThrowIfNull(nameof(account));

            _unitOfWork.AccountRepository.Insert(account);
            _unitOfWork.SaveChanges();
            OnAccountOpened(account);
        }

        public async Task OpenNewAccountAsync(string personalNumber, string accountNumber, decimal balance, CancellationToken token)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(personalNumber);
            ArgumentException.ThrowIfNullOrWhiteSpace(accountNumber);
            ArgumentOutOfRangeException.ThrowIfNegative(balance);

            Customer? customer = (await _unitOfWork.CustomerRepository.QueryAsync(c => c.PersonalNumber.Equals(personalNumber), token)).FirstOrDefault();

            ArgumentNullException.ThrowIfNull(customer);

            Account account = new()
            {
                Customer = customer,
                AccountNumber = accountNumber,
                Balance = balance,
                Status = AccountStatus.Active
            };

            ArgumentNullException.ThrowIfNull(nameof(account));

            await _unitOfWork.AccountRepository.InsertAsync(account, token);
            await _unitOfWork.SavechangesAsync(token);
            OnAccountOpened(account);
        }

        public decimal CheckBalance(string accountNumber)
        {
            Account? account = _unitOfWork.AccountRepository.Query(a => a.AccountNumber.Equals(accountNumber)).FirstOrDefault();

            ArgumentNullException.ThrowIfNull(account);

            return account.Balance;
        }

        public async Task<decimal> CheckBalanceAsync(string accountNumber, CancellationToken token)
        {
            Account? account = (await _unitOfWork.AccountRepository.QueryAsync(a => a.AccountNumber == (accountNumber), token)).FirstOrDefault();

            ArgumentNullException.ThrowIfNull(account);

            return account.Balance;
        }

        public void FreezeAccount(string accountNumber)
        {
            Account? account = _unitOfWork.AccountRepository.Query(a => a.AccountNumber.Equals(accountNumber)).FirstOrDefault();

            ArgumentNullException.ThrowIfNull(account);

            if (account.Status == AccountStatus.Closed)
                throw new InvalidOperationException("Closed account cannot be frozen.");

            if (account.Status == AccountStatus.Frozen)
                throw new InvalidOperationException("Account is already frozen.");

            account.Status = AccountStatus.Frozen;
            _unitOfWork.AccountRepository.Update(account);
            _unitOfWork.SaveChanges();
            OnAccountUpdated(account);
        }

        public async Task FreezeAccountAsync(string accountNumber, CancellationToken token)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(nameof(accountNumber));

            Account? account = (await _unitOfWork.AccountRepository.QueryAsync(a => a.AccountNumber.Equals(accountNumber), token)).FirstOrDefault();

            ArgumentNullException.ThrowIfNull(account);

            if (account.Status == AccountStatus.Closed)
                throw new InvalidOperationException("Closed account cannot be frozen.");

            if (account.Status == AccountStatus.Frozen)
                throw new InvalidOperationException("Account is already frozen.");

            account.Status = AccountStatus.Frozen;

            await _unitOfWork.AccountRepository.UpdateAsync(account, token);
            await _unitOfWork.SavechangesAsync(token);
            OnAccountUpdated(account);
        }

        public void UnfreezeAccount(string accountNumber)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(nameof(accountNumber));

            Account? account = _unitOfWork.AccountRepository.Query(a => a.AccountNumber.Equals(accountNumber)).FirstOrDefault();

            ArgumentNullException.ThrowIfNull(account);

            if (account.Status == AccountStatus.Active)
                throw new InvalidOperationException("Account is already activated.");

            account.Status = AccountStatus.Active;

            _unitOfWork.AccountRepository.Update(account);
            _unitOfWork.SaveChanges();
            OnAccountUpdated(account);
        }

        public async Task UnfreezeAccountAsync(string accountNumber, CancellationToken token)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(nameof(accountNumber));
            
            Account? account = (await _unitOfWork.AccountRepository.QueryAsync(a => a.AccountNumber.Equals(accountNumber), token)).FirstOrDefault();

            ArgumentNullException.ThrowIfNull(account);

            if (account.Status == AccountStatus.Active)
                throw new InvalidOperationException("Account is already activated.");

            account.Status = AccountStatus.Active;

            await _unitOfWork.AccountRepository.UpdateAsync(account, token);
            await _unitOfWork.SavechangesAsync(token);
            OnAccountUpdated(account);
        }

        public void CloseAccount(string accountNumber)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(nameof(accountNumber));
            
            Account? account = _unitOfWork.AccountRepository.Query(a => a.AccountNumber.Equals(accountNumber))!.FirstOrDefault();

            ArgumentNullException.ThrowIfNull(account);

            _unitOfWork.AccountRepository.Delete(account);
            _unitOfWork.SaveChanges();
            OnAccountClosed(account);
        }

        public async Task CloseAccountAsync(string accountNumber, CancellationToken token)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(nameof(accountNumber));
            
            Account? account = (await _unitOfWork.AccountRepository.QueryAsync(a => a.AccountNumber.Equals(accountNumber), token)).FirstOrDefault();

            ArgumentNullException.ThrowIfNull(account);

            await _unitOfWork.AccountRepository.DeleteAsync(account, token);
            await _unitOfWork.SavechangesAsync(token);
            OnAccountClosed(account);
        }

        private void OnAccountOpened(Account account)
        {
            AccountOpened?.Invoke(account);
        }

        private void OnAccountUpdated(Account account)
        {
            AccountUpdated?.Invoke(account);
        }

        private void OnAccountClosed(Account account)
        {
            AccountClosed?.Invoke(account);
        }
    }
}
