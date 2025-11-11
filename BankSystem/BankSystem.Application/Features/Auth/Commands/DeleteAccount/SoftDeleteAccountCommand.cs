using BankSystem.Application.Common.Interfaces.Services;
using BankSystem.Shared.Models;
using MediatR;

namespace BankSystem.Application.Features.Auth.Commands.DeleteAccount;

public record SoftDeleteAccountCommand(int AccountId) : IRequest<Result<Unit>>;

public class SoftDeleteAccountHandler(IAccountService accountService) : IRequestHandler<SoftDeleteAccountCommand, Result<Unit>>
{
    private readonly IAccountService _accountService = accountService;
    public async Task<Result<Unit>> Handle(SoftDeleteAccountCommand command, CancellationToken cancellationToken)
    {
        return await _accountService.SoftDeleteAccountAsync(command.AccountId, cancellationToken);
    }
}
