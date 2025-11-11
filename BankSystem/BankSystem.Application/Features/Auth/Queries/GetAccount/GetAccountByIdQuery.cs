using AutoMapper;
using BankSystem.Application.Common.DTOs.Account;
using BankSystem.Application.Common.Interfaces.Services;
using BankSystem.Shared.Models;
using MediatR;

namespace BankSystem.Application.Features.Auth.Queries.GetAccount;
public record GetAccountByIdQuery(int AccountId) : IRequest<Result<GetAccountResponseDto?>>;

public class GetAccountByIdHandler(IAccountService accountService, IMapper mapper) : IRequestHandler<GetAccountByIdQuery, Result<GetAccountResponseDto?>>
{
    private readonly IAccountService _accountService = accountService;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<GetAccountResponseDto?>> Handle(GetAccountByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await _accountService.GetAccountByIdAsync(query.AccountId, cancellationToken);

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