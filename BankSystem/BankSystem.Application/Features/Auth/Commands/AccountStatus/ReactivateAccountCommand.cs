using AutoMapper;
using BankSystem.Application.Common.DTOs.Account;
using BankSystem.Application.Common.Interfaces.Services;
using BankSystem.Shared.Models;
using MediatR;

namespace BankSystem.Application.Features.Auth.Commands.AccountStatus;

public record ReactivateAccountCommand(int AccountId) : IRequest<Result<GetAccountResponseDto>>;

public class ReactivateAccountHandler(IMapper mapper, IAccountService accountService) : IRequestHandler<ReactivateAccountCommand, Result<GetAccountResponseDto>>
{
    private readonly IAccountService _accountService = accountService;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<GetAccountResponseDto>> Handle(ReactivateAccountCommand request, CancellationToken cancellationToken)
    {
        var result = await _accountService.ReactivateAccountAsync(request.AccountId, cancellationToken);

        var response = _mapper.Map<GetAccountResponseDto>(result.Data);

        return new Result<GetAccountResponseDto>
        {
            Code = result.Code,
            Succeeded = result.Succeeded,
            Data = response,
            Messages = result.Messages
        };
    }
}