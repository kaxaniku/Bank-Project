using BankSystem.Application.Common.Interfaces.Services;
using BankSystem.Shared.Models;
using MediatR;

namespace BankSystem.Application.Features.Auth.Commands.DeleteCustomer;

public record SoftDeleteCustomerCommand(int CustomerId) : IRequest<Result<Unit>>;

public class SoftDeleteCustomerHandler(ICustomerService customerService) : IRequestHandler<SoftDeleteCustomerCommand, Result<Unit>>
{
    private readonly ICustomerService _customerService = customerService;

    public async Task<Result<Unit>> Handle(SoftDeleteCustomerCommand command, CancellationToken cancellationToken)
    {
        return await _customerService.SoftDeleteCustomerAsync(command.CustomerId, cancellationToken);
    }
}