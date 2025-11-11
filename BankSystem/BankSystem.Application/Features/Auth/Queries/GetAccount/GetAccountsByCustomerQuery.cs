using AutoMapper;
using BankSystem.Application.Common.DTOs.Account;
using BankSystem.Application.Common.Interfaces.Services;
using BankSystem.Shared.Models;
using MediatR;

namespace BankSystem.Application.Features.Auth.Queries.GetAccount;

public record GetAccountsByCustomerQuery(int CustomerId) : IRequest<Result<IEnumerable<GetAccountResponseDto>>>;

public class GetAccountsByCustomerHandler(IAccountService accountService, IMapper mapper) : IRequestHandler<GetAccountsByCustomerQuery, Result<IEnumerable<GetAccountResponseDto>>>
{
    private readonly IAccountService _accountService = accountService;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<IEnumerable<GetAccountResponseDto>>> Handle(
        GetAccountsByCustomerQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _accountService.GetAccountByCustomerIdAsync(query.CustomerId, cancellationToken);

        var accounts = _mapper.Map<IEnumerable<GetAccountResponseDto>>(result.Data);

        return new Result<IEnumerable<GetAccountResponseDto>>
        {
            Succeeded = result.Succeeded,
            Data = accounts,
            Code = result.Code,
            Messages = result.Messages
        };
    }
}