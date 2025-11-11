using AutoMapper;
using BankSystem.Application.Common.DTOs.Customer;
using BankSystem.Application.Common.Interfaces.Services;
using BankSystem.Domain.Entities;
using BankSystem.Shared.Models;
using MediatR;

namespace BankSystem.Application.Features.Auth.Commands.UpdateCustomer;

public record UpdateCustomerCommand(int CustomerId, UpdateCustomerRequestDto Request) : IRequest<Result<CustomerResponseDto>>;

public class UpdateCustomerHandler(ICustomerService customerService, IMapper mapper) : IRequestHandler<UpdateCustomerCommand, Result<CustomerResponseDto>>
{
    private readonly ICustomerService _customerService = customerService;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<CustomerResponseDto>> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
    {
        var customerUpdates = _mapper.Map<Customer>(command.Request);
        customerUpdates.Id = command.CustomerId;

        var result = await _customerService.UpdateCustomerAsync(customerUpdates, cancellationToken);

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