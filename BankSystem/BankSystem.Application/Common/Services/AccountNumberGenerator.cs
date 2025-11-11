using BankSystem.Application.Common.Interfaces.Repositories;
using BankSystem.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace BankSystem.Application.Common.Services;

public class AccountNumberGenerator(IUnitOfWork unitOfWork, ILogger<AccountNumberGenerator> logger) : IAccountNumberGenerator
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<AccountNumberGenerator> _logger = logger;
    private const int MaxRetries = 10;
    private const int AccountNumberLength = 10;

    public async Task<string> GenerateUniqueAccountNumberAsync(CancellationToken cancellationToken = default)
    {
        for (int attempt = 0; attempt < MaxRetries; attempt++)
        {
            var accountNumber = GenerateAccountNumber();

            var existingAccount = await _unitOfWork.Accounts.GetByAccountNumberAsync(accountNumber, cancellationToken);

            if (existingAccount == null)
            {
                _logger.LogInformation("Generated unique account number: {AccountNumber}", accountNumber);
                return accountNumber;
            }

            _logger.LogWarning("Account number collision detected: {AccountNumber}. Retry attempt {Attempt}", accountNumber, attempt + 1);
        }

        _logger.LogError("Failed to generate unique account number after {MaxRetries} attempts", MaxRetries);
        throw new InvalidOperationException($"Failed to generate unique account number after {MaxRetries} attempts");
    }

    private static string GenerateAccountNumber()
    {
        var random = new Random();
        var accountNumber = new char[AccountNumberLength];

        for (int i = 0; i < AccountNumberLength; i++)
        {
            accountNumber[i] = (char)('0' + random.Next(0, 10));
        }

        return new string(accountNumber);
    }
}