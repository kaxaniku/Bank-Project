using BankSystem.Application.Common.DTOs.Account;
using BankSystem.Application.Common.Interfaces.Services;
using BankSystem.Shared.Models;
using MediatR;

namespace BankSystem.Application.Features.Auth.Queries.GetAccount;

public record GetAccountBalanceQuery(int AccountId) : IRequest<Result<IEnumerable<BalancePerCurrency>>>;

public class GetAccountBalanceHandler(IAccountService accountService) : IRequestHandler<GetAccountBalanceQuery, Result<IEnumerable<BalancePerCurrency>>>
{
    private readonly IAccountService _accountService = accountService;

    public async Task<Result<IEnumerable<BalancePerCurrency>>> Handle(GetAccountBalanceQuery query, CancellationToken cancellationToken)
    {
        var balance = await _accountService.GetAccountBalanceAsync(query.AccountId, cancellationToken);

        return new Result<IEnumerable<BalancePerCurrency>>
        {
            Succeeded = balance.Succeeded,
            Data = balance.Data,
            Code = balance.Code,
            Messages = balance.Messages
        };
    }
}