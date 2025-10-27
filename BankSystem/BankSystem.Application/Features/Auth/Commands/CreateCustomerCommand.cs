using AutoMapper;
using BankSystem.Application.Common.DTOs;
using BankSystem.Application.Common.Interfaces.Services;
using BankSystem.Domain.Entities;
using BankSystem.Shared.Models;
using MediatR;

namespace BankSystem.Application.Features.Auth.Commands;

public record CreateCustomerCommand(CreateCustomerRequestDto Request) : IRequest<Result<CreateCustomerResponseDto>>;

public class CreateCustomerHandler(ICustomerService customerService, IMapper mapper) : IRequestHandler<CreateCustomerCommand, Result<CreateCustomerResponseDto>>
{
    private readonly ICustomerService _customerService = customerService;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<CreateCustomerResponseDto>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = _mapper.Map<Customer>(command.Request);
        var result = await _customerService.CreateCustomerAsync(customer, cancellationToken);

        if (!result.Succeeded)
        {
            return new Result<CreateCustomerResponseDto>
            {
                Code = result.Code,
                Succeeded = false,
                Messages = result.Messages
            };
        }

        var response = _mapper.Map<CreateCustomerResponseDto>(result.Data);
        return new Result<CreateCustomerResponseDto>
        {
            Code = 200,
            Succeeded = true,
            Data = response
        };
    }
}