using BankSystem.Application.Common.Interfaces.Repositories;
using BankSystem.Application.Common.Interfaces.Services;
using BankSystem.Domain.Entities;
using BankSystem.Shared.Models;
using Microsoft.Extensions.Logging;

namespace BankSystem.Application.Common.Services;

public class CustomerService(IUnitOfWork unitOfWork, ILogger<CustomerService> logger) : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<CustomerService> _logger = logger;

    public async Task<Result<Customer>> CreateCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new customer with email: {Email}", customer.Email);
        var existingCustomer = await _unitOfWork.Customers.GetByEmailAsync(customer.Email, cancellationToken);
        if (existingCustomer != null)
        {
            _logger.LogInformation("Attempt to create duplicate customer with email: {Email}", customer.Email);
            return new Result<Customer>
            {
                Succeeded = false,
                Code = 409,
                Messages = ["Customer with this email already exists."]
            };
        }

        await _unitOfWork.Customers.AddAsync(customer, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Created customer with ID: {CustomerId}", customer.Id);

        return new Result<Customer>
        {
            Succeeded = true,
            Code = 200,
            Data = customer
        };
    }

    public async Task<Customer?> GetCustomerWithAccountsAsync(int customerId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving customer with ID: {CustomerId} and their accounts", customerId);
        return await _unitOfWork.Customers.GetWithAccountsAsync(customerId, cancellationToken);
    }

    public async Task<(IEnumerable<Customer> Customers, int TotalCount)> GetCustomersPagedAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving customers page {PageNumber} with page size {PageSize}", pageNumber, pageSize);
        return await _unitOfWork.Customers.GetPagedAsync(pageNumber, pageSize, cancellationToken: cancellationToken);
    }

    public async Task UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating customer with ID: {CustomerId}", customer.Id);
        var existingCustomer = await _unitOfWork.Customers.GetByEmailAsync(customer.Email, cancellationToken);
        if (existingCustomer != null)
        {
            _logger.LogInformation("Attempt to create duplicate customer with email: {Email}", customer.Email);
            throw new InvalidOperationException($"Email {customer.Email} is already in use.");
        }

        _unitOfWork.Customers.Update(customer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated customer with ID: {CustomerId}", customer.Id);
    }

    public async Task<bool> DeleteCustomerAsync(int customerId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting customer with ID: {CustomerId}", customerId);
        var customer = await _unitOfWork.Customers.FirstOrDefaultAsync(c => c.Id == customerId, cancellationToken);
        if (customer == null)
        {
            _logger.LogInformation("Attempt to delete non-existing customer with ID: {CustomerId}", customerId);
            return false;
        }

        var accounts = await _unitOfWork.Accounts.GetCustomerAccountsAsync(customerId, cancellationToken);
        if (accounts.Any())
        {
            _logger.LogInformation("Attempt to delete customer with existing accounts. Customer ID: {CustomerId}", customerId);
            throw new InvalidOperationException("Cannot delete customer with existing accounts.");
        }

        _unitOfWork.Customers.Remove(customer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted customer with ID: {CustomerId}", customerId);
        return true;
    }
}