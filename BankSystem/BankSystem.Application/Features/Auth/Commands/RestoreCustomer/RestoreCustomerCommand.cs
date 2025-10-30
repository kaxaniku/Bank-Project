using BankSystem.Application.Common.Interfaces.Services;
using BankSystem.Shared.Models;
using MediatR;

namespace BankSystem.Application.Features.Auth.Commands.RestoreCustomer;

public record RestoreCustomerCommand(int CustomerId) : IRequest<Result<Unit>>;

public class RestoreCustomerHandler(ICustomerService customerService) : IRequestHandler<RestoreCustomerCommand, Result<Unit>>
{
    private readonly ICustomerService _customerService = customerService;

    public async Task<Result<Unit>> Handle(RestoreCustomerCommand command, CancellationToken cancellationToken)
    {
        return await _customerService.RestoreCustomerAsync(command.CustomerId, cancellationToken);
    }
}