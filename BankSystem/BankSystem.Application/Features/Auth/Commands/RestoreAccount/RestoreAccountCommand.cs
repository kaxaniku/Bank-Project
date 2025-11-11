using BankSystem.Application.Common.Interfaces.Services;
using BankSystem.Shared.Models;
using MediatR;

namespace BankSystem.Application.Features.Auth.Commands.RestoreAccount;

public record RestoreAccountCommand(int AccountId) : IRequest<Result<Unit>>;

public class RestoreAccountHandler(IAccountService accountService) : IRequestHandler<RestoreAccountCommand, Result<Unit>>
{
    private readonly IAccountService _accountService = accountService;

    public async Task<Result<Unit>> Handle(RestoreAccountCommand command, CancellationToken cancellationToken)
    {
        return await _accountService.RestoreAccountAsync(command.AccountId, cancellationToken);
    }
}