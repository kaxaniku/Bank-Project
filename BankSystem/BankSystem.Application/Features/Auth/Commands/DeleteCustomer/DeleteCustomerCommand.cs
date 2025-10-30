using BankSystem.Application.Common.Interfaces.Services;
using BankSystem.Shared.Models;
using MediatR;

namespace BankSystem.Application.Features.Auth.Commands.DeleteCustomer;

public record DeleteCustomerCommand(int CustomerId) : IRequest<Result<Unit>>;

public class DeleteCustomerHandler(ICustomerService customerService) : IRequestHandler<DeleteCustomerCommand, Result<Unit>>
{
    private readonly ICustomerService _customerService = customerService;

    public async Task<Result<Unit>> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
    {
        return await _customerService.HardDeleteCustomerAsync(command.CustomerId, cancellationToken);
    }
}