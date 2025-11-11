using AutoMapper;
using BankSystem.Application.Common.DTOs.Customer;
using BankSystem.Application.Common.Interfaces.Services;
using BankSystem.Domain.Entities;
using BankSystem.Shared.Models;
using MediatR;

namespace BankSystem.Application.Features.Auth.Commands.CreateCustomer;

public record CreateCustomerCommand(CreateCustomerRequestDto Request) : IRequest<Result<CustomerResponseDto>>;

public class CreateCustomerHandler(ICustomerService customerService, IMapper mapper) : IRequestHandler<CreateCustomerCommand, Result<CustomerResponseDto>>
{
    private readonly ICustomerService _customerService = customerService;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<CustomerResponseDto>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = _mapper.Map<Customer>(command.Request);

        var result = await _customerService.CreateCustomerAsync(customer, cancellationToken);

        var response = _mapper.Map<CustomerResponseDto>(result.Data);
        return new Result<CustomerResponseDto>
        {
            Code = result.Code,
            Succeeded = result.Succeeded,
            Data = response,
            Messages = result.Messages
        };
    }
}