using MyBank.Application.Interfaces.Repositories;
using MyBank.Application.Interfaces.Services;
using MyBank.Domain;

namespace MyBank.Application;

public sealed class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public void RegisterNewCustomer(Customer customer)
    {
        throw new NotImplementedException();
    }

    public void UpdateCustomer(Customer customer)
    {
        throw new NotImplementedException();
    }

    public void RemoveCustomer(int customerId)
    {
        throw new NotImplementedException();
    }

    public Customer? FindCustomerById(int customerId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Customer> ListAllCustomers()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<int> ListAccountsByCustomer(int customerId)
    {
        throw new NotImplementedException();
    }

    public Task RegisterNewCustomerAsync(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCustomerAsync(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task RemoveCustomerAsync(int customerId)
    {
        throw new NotImplementedException();
    }

    public Task<Customer?> FindCustomerByIdAsync(int customerId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Customer>> ListAllCustomersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<int>> ListAccountsByCustomerAsync(int customerId)
    {
        throw new NotImplementedException();
    }
}
