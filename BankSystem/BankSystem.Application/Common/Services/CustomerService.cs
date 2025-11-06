using BankSystem.Application.Common.Interfaces.Repositories;
using BankSystem.Application.Common.Interfaces.Services;
using BankSystem.Domain.Entities;
using BankSystem.Shared.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BankSystem.Application.Common.Services;

public class CustomerService(IUnitOfWork unitOfWork, ILogger<CustomerService> logger) : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<CustomerService> _logger = logger;

    public async Task<Result<Customer>> CreateCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new customer with email: {Email}", customer.Email);
        var existingCustomer = await _unitOfWork.Customers.GetByEmailOrNationalIdAsync(customer.Email, customer.NationalId, cancellationToken);

        if (existingCustomer != null)
        {
            _logger.LogInformation("Attempt to create duplicate customer with email: {Email}", customer.Email);
            return BuildResult<Customer>(404, false, messages: ["Customer with this email or national id already exists."]);
        }

        await _unitOfWork.Customers.AddAsync(customer, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Created customer with ID: {CustomerId}", customer.Id);

        return BuildResult(201, true, customer);
    }

    public async Task<Result<Customer?>> GetCustomerWithAccountsAsync(int customerId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving customer with ID: {CustomerId} and their accounts", customerId);
        
        var customer = await _unitOfWork.Customers.GetWithAccountsAsync(customerId, cancellationToken);
        return customer == null ? BuildResult<Customer?>(404, false, messages: ["Customer not found."]) : BuildResult<Customer?>(200, true, customer);
    }

    public async Task<Result<Customer?>> GetCustomerWithAccountsAsync(string nationalId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving customer with ID: {NationalId} and their accounts", nationalId);

        var customer = await _unitOfWork.Customers.GetByNationalIdAsync(nationalId, cancellationToken);
        return customer == null ? BuildResult<Customer?>(404, false, messages: ["Customer not found."]) : BuildResult<Customer?>(200, true, customer);
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
            return BuildResult<Customer>(404, false, messages: ["Customer not found."]);
        }

        ApplyCustomerUpdates(customer, existingCustomer);

        _unitOfWork.Customers.Update(existingCustomer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Successfully updated customer with Id: {CustomerId}", customer.Id);

        return BuildResult(200, true, existingCustomer);
    }

    public async Task<Result<Unit>> HardDeleteCustomerAsync(int customerId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting customer with ID: {CustomerId}", customerId);
        var customer = await _unitOfWork.Customers.FirstOrDefaultAsync(c => c.Id == customerId, cancellationToken);
        if (customer == null)
        {
            _logger.LogInformation("Attempt to delete non-existing customer with ID: {CustomerId}", customerId);
            return BuildResult<Unit>(404, false, messages: ["Customer doesn't exist."]);
        }

        var accounts = await _unitOfWork.Accounts.GetCustomerAccountsAsync(customerId, cancellationToken);
        if (accounts.Any())
        {
            _logger.LogInformation("Attempt to delete customer with existing accounts. Customer ID: {CustomerId}", customerId);
            return BuildResult<Unit>(409, false, messages: ["Can't delete customer with existing accounts."]);
        }

        _unitOfWork.Customers.Remove(customer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted customer with ID: {CustomerId}", customerId);
        return BuildResult(200, true, new Unit());
    }

    public async Task<Result<Unit>> SoftDeleteCustomerAsync(int customerId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Soft deleting customer with ID: {CustomerId}", customerId);
        var customer = await _unitOfWork.Customers.FirstOrDefaultAsync(c => c.Id == customerId, cancellationToken);
        if (customer == null)
        {
            _logger.LogInformation("Attempt to soft delete non-existing customer with ID: {CustomerId}", customerId);
            return BuildResult<Unit>(404, false, messages: ["Customer doesn't exist."]);
        }

        var activeAccounts = await _unitOfWork.Accounts.GetAllAsync(a => a.CustomerId == customerId && a.IsActive, cancellationToken: cancellationToken);
        if (activeAccounts.Any())
        {
            _logger.LogInformation("Attempt to soft delete customer with existing accounts. Customer ID: {CustomerId}", customerId);
            return BuildResult<Unit>(409, false, messages: ["Can't delete customer with active accounts. Please close all accounts first."]);
        }

        if (!customer.IsActive)
        {
            return BuildResult<Unit>(409, false, messages: ["Customer is already inactive."]);
        }

        _unitOfWork.Customers.SoftDelete(customer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Soft deleted customer with ID: {CustomerId}", customerId);
        return BuildResult(200, true, new Unit());
    }

    public async Task<Result<Unit>> RestoreCustomerAsync(int customerId, CancellationToken cancellationToken = default)
    {
        var customer = await _unitOfWork.Customers.FirstOrDefaultAsync(c => c.Id == customerId, cancellationToken);
        if (customer == null)
        {
            return BuildResult<Unit>(404, false, messages: ["Customer doesn't exist."]);
        }

        if (customer.IsActive)
        {
            return BuildResult<Unit>(409, false, messages: ["Customer is already active."]);
        }

        _unitOfWork.Customers.Restore(customer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Customer restored. ID: {CustomerId}", customerId);
        return BuildResult(200, true, new Unit());
    }

    private static Result<T> BuildResult<T>(int code, bool succeeded, T? data = default, List<string>? messages = null)
    {
        return new Result<T>
        {
            Succeeded = succeeded,
            Code = code,
            Data = data ?? default!,
            Messages = messages ?? []
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