namespace BankSystem.Application.Features.Auth.Commands.DeleteCustomer;

//public record DeleteCustomerCommand(int CustomerId) : IRequest<Result<Unit>>;

//public class DeleteCustomerHandler(ICustomerService customerService, IMapper mapper) : IRequestHandler<DeleteCustomerCommand, Result<Unit>>
//{
//    private readonly ICustomerService _customerService = customerService;
//    private readonly IMapper _mapper = mapper;

//    public async Task<Result<Unit>> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
//    {
//        var result = await _customerService.DeleteCustomerAsync(command.CustomerId, cancellationToken);

//        var response = _mapper.Map<CustomerResponseDto>(result.Data);
//        return new Result<CustomerResponseDto>
//        {
//            Code = result.Code,
//            Succeeded = result.Succeeded,
//            Data = response,
//            Messages = result.Messages
//        };
//    }
//}