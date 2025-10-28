using AutoMapper;
using BankSystem.Application.Common.DTOs;
using BankSystem.Application.Common.Interfaces.Services;
using BankSystem.Shared.Models;
using MediatR;

namespace BankSystem.Application.Features.Auth.Commands.GetCustomer;

public record GetCustomerByIdCommand(int CustomerId) : IRequest<Result<GetCustomerResponseDto?>>;

public class GetCustomerByIdHandler(ICustomerService customerService, IMapper mapper) : IRequestHandler<GetCustomerByIdCommand, Result<GetCustomerResponseDto?>>
{
    private readonly ICustomerService _customerService = customerService;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<GetCustomerResponseDto?>> Handle(GetCustomerByIdCommand command, CancellationToken cancellationToken)
    {
        var result = await _customerService.GetCustomerWithAccountsAsync(command.CustomerId, cancellationToken);

        var customer = _mapper.Map<GetCustomerResponseDto?>(result.Data);

        return new Result<GetCustomerResponseDto?>
        {
            Succeeded = result.Succeeded,
            Data = customer,
            Code = result.Code,
            Messages = result.Messages
        };
    }
}