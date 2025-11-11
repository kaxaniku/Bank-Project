namespace BankSystem.Application.Common.Interfaces.Services;

public interface IAccountNumberGenerator
{
    Task<string> GenerateUniqueAccountNumberAsync(CancellationToken cancellationToken = default);
}