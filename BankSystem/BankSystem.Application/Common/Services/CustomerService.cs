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
        existingCustomer ??= await _unitOfWork.Customers.GetByNationalIdAsync(customer.NationalId, cancellationToken);

        if (existingCustomer != null)
        {
            _logger.LogInformation("Attempt to create duplicate customer with email: {Email}", customer.Email);
            return BuildFailureResult<Customer>(409, ["Customer with this email or national id already exists."]);
        }

        await _unitOfWork.Customers.AddAsync(customer, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Created customer with ID: {CustomerId}", customer.Id);

        return BuildSuccessResult<Customer>(200, customer);
    }

    public async Task<Result<Customer?>> GetCustomerWithAccountsAsync(int customerId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving customer with ID: {CustomerId} and their accounts", customerId);
        
        var customer = await _unitOfWork.Customers.GetWithAccountsAsync(customerId, cancellationToken);
        return customer == null ? BuildFailureResult<Customer?>(404, ["Customer not found."]) : BuildSuccessResult<Customer?>(200, customer);
    }

    public async Task<Result<Customer?>> GetCustomerWithAccountsAsync(string nationalId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving customer with ID: {NationalId} and their accounts", nationalId);

        var customer = await _unitOfWork.Customers.GetByNationalIdAsync(nationalId, cancellationToken);
        return customer == null ? BuildFailureResult<Customer?>(404, ["Customer not found."]) : BuildSuccessResult<Customer?>(200, customer);
    }

    public async Task<(IEnumerable<Customer> Customers, int TotalCount)> GetCustomersPagedAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving customers page {PageNumber} with page size {PageSize}", pageNumber, pageSize);
        return await _unitOfWork.Customers.GetPagedAsync(pageNumber, pageSize, cancellationToken: cancellationToken);
    }

    public async Task<Result<Customer>> UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating customer with ID: {CustomerId}", customer.Id);

        var existingCustomer = await _unitOfWork.Customers.FirstOrDefaultAsync(c => c.Id == customer.Id, cancellationToken);

        if (existingCustomer == null)
        {
            _logger.LogWarning("Customer with ID {CustomerId} not found", customer.Id);
            return BuildFailureResult<Customer>(404, [$"Customer with ID {customer.Id} not found."]);
        }

        ApplyCustomerUpdates(customer, existingCustomer);

        _unitOfWork.Customers.Update(existingCustomer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Successfully updated customer with ID: {CustomerId}", customer.Id);

        return BuildSuccessResult(200, existingCustomer);
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

    private static Result<T> BuildFailureResult<T>(int code, List<string> messages)
    {
        return new Result<T>
        {
            Succeeded = false,
            Code = code,
            Messages = messages
        };
    }
    
    private static Result<T> BuildSuccessResult<T>(int code, T data)
    {
        return new Result<T>
        {
            Succeeded = true,
            Code = code,
            Data = data
        };
    }

    private static void ApplyCustomerUpdates(Customer customer, Customer existingCustomer)
    {
        existingCustomer.FirstName = customer.FirstName;
        existingCustomer.LastName = customer.LastName;
        existingCustomer.PhoneNumber = customer.PhoneNumber;
        existingCustomer.Address = customer.Address;
        existingCustomer.DateOfBirth = customer.DateOfBirth;
        existingCustomer.Email = customer.Email;
        existingCustomer.UpdatedAt = DateTime.UtcNow;
    }
}