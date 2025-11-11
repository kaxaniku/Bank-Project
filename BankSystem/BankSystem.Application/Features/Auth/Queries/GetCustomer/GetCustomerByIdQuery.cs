using AutoMapper;
using BankSystem.Application.Common.DTOs.Customer;
using BankSystem.Application.Common.Interfaces.Services;
using BankSystem.Shared.Models;
using MediatR;

namespace BankSystem.Application.Features.Auth.Queries.GetCustomer;

public record GetCustomerByIdQuery(int CustomerId) : IRequest<Result<GetCustomerResponseDto?>>;

public class GetCustomerByIdHandler(ICustomerService customerService, IMapper mapper) : IRequestHandler<GetCustomerByIdQuery, Result<GetCustomerResponseDto?>>
{
    private readonly ICustomerService _customerService = customerService;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<GetCustomerResponseDto?>> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await _customerService.GetCustomerWithAccountsAsync(query.CustomerId, cancellationToken);

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