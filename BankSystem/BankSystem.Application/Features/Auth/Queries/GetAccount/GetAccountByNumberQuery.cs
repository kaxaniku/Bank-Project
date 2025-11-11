using AutoMapper;
using BankSystem.Application.Common.DTOs.Account;
using BankSystem.Application.Common.Interfaces.Services;
using BankSystem.Shared.Models;
using MediatR;

namespace BankSystem.Application.Features.Auth.Queries.GetAccount;
public record GetAccountByNumberQuery(string AccountNumber) : IRequest<Result<GetAccountResponseDto?>>;

public class GetAccountByNumberHandler(IAccountService accountService, IMapper mapper) : IRequestHandler<GetAccountByNumberQuery, Result<GetAccountResponseDto?>>
{
    private readonly IAccountService _accountService = accountService;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<GetAccountResponseDto?>> Handle(GetAccountByNumberQuery query, CancellationToken cancellationToken)
    {
        var result = await _accountService.GetAccountByNumberAsync(query.AccountNumber, cancellationToken);

        var account = _mapper.Map<GetAccountResponseDto?>(result.Data);

        return new Result<GetAccountResponseDto?>
        {
            Succeeded = result.Succeeded,
            Data = account,
            Code = result.Code,
            Messages = result.Messages
        };
    }
}