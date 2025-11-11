using AutoMapper;
using BankSystem.Application.Common.DTOs.Account;
using BankSystem.Application.Common.Interfaces.Services;
using BankSystem.Domain.Entities;
using BankSystem.Shared.Models;
using MediatR;

namespace BankSystem.Application.Features.Auth.Commands.CreateAccount;

public record CreateAccountCommand(CreateAccountRequestDto Request) : IRequest<Result<CreateAccountResponseDto>>;

public class CreateAccountHandler(IMapper mapper, IAccountService accountService) : IRequestHandler<CreateAccountCommand, Result<CreateAccountResponseDto>>
{
    private readonly IAccountService _accountService = accountService;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<CreateAccountResponseDto>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = _mapper.Map<Account>(request.Request);

        var result = await _accountService.CreateAccountAsync(account, cancellationToken);

        var response = _mapper.Map<CreateAccountResponseDto>(result.Data);

        return new Result<CreateAccountResponseDto>
        {
            Code = result.Code,
            Succeeded = result.Succeeded,
            Data = response,
            Messages = result.Messages
        };
    }
}