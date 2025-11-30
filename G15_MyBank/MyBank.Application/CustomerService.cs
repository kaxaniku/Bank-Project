using MyBank.Application.Interfaces.Repositories;
using MyBank.Application.Interfaces.Services;
using MyBank.Domain;

namespace MyBank.Application
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public event Action<Customer>? CustomerCreated;
        public event Action<Customer>? Customerupdated;
        public event Action<int>? CustomerRemoved;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Customer? FindCustomerById(int customerId)
        {
            Customer customer = _unitOfWork.CustomerRepository.GetById(customerId)!;

            if (customer == null)
                throw new KeyNotFoundException($"Customer with Id {customerId} not found");

            return customer;
        }

        public async Task<Customer?> FindCustomerByIdAsync(int customerId, CancellationToken token)
        {
            Customer customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId, token);

            if (customer == null)
                throw new KeyNotFoundException($"Customer with Id {customerId} not found");

            return customer;
        }

        public IEnumerable<Account> GetAccountsByCustomer(int customerId)
        {
            return _unitOfWork.AccountRepository.Query(a => a.Customer.CustomerId == customerId).ToList();
        }

        public async Task<IEnumerable<Account>> GetAccountsByCustomerAsync(int customerId, CancellationToken token)
        {
            return await _unitOfWork.AccountRepository.QueryAsync(a => a.Customer.CustomerId == customerId, token);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _unitOfWork.CustomerRepository.Query(c => c.Activity.IsActive).ToList();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync(CancellationToken token)
        {
            return await _unitOfWork.CustomerRepository.QueryAsync(c => c.Activity.IsActive, token);
        }

        public void RegisterNewCustomer(Customer customer)
        {
            if (customer == null)
                throw new InvalidOperationException("Customer cannot be null");

            _unitOfWork.CustomerRepository.Insert(customer);
            _unitOfWork.SaveChanges();
            OnCustomerCreated(customer);
        }

        public async Task RegisterNewCustomerAsync(Customer customer, CancellationToken token)
        {
            if (customer == null)
                throw new InvalidOperationException("Customer cannot be null");

            await _unitOfWork.CustomerRepository.InsertAsync(customer, token);
            await _unitOfWork.SavechangesAsync(token);
            OnCustomerCreated(customer);
        }

        public void RemoveCustomer(int customerId)
        {
            Customer customer = _unitOfWork.CustomerRepository.GetById(customerId)!;

            if (customer == null)
                throw new KeyNotFoundException($"Customer with Id {customerId} not found");

            _unitOfWork.CustomerRepository.Delete(customer);
            _unitOfWork.SaveChanges();
            OnCustomerRemoved(customerId);
        }

        public async Task RemoveCustomerAsync(int customerId, CancellationToken token)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId, token);

            if (customer == null)
                throw new KeyNotFoundException($"Customer with Id {customerId} not found");

            await _unitOfWork.CustomerRepository.DeleteAsync(customer, token);
            await _unitOfWork.SavechangesAsync(token);
            OnCustomerRemoved(customerId);
        }

        public void UpdateCustomer(Customer customer)
        {
            if (customer == null)
                throw new KeyNotFoundException($"Customer not found");

            _unitOfWork.CustomerRepository.Update(customer);
            _unitOfWork.SaveChanges();
            OnCustomerUpdated(customer);
        }

        public async Task UpdateCustomerAsync(Customer customer, CancellationToken token)
        {
            if (customer == null)
                throw new KeyNotFoundException($"Customer not found");

            await _unitOfWork.CustomerRepository.UpdateAsync(customer, token);
            await _unitOfWork.SavechangesAsync(token);
            OnCustomerUpdated(customer);
        }

        private void OnCustomerCreated(Customer customer)
        {
            CustomerCreated?.Invoke(customer);
        }

        private void OnCustomerUpdated(Customer customer)
        {
            Customerupdated?.Invoke(customer);
        }

        private void OnCustomerRemoved(int customerId)
        {
            CustomerRemoved?.Invoke(customerId);
        } 
    }
}
