using BankSystem.Application.Common.DTOs.Account;
using BankSystem.Application.Common.DTOs.Customer;
using BankSystem.Application.Features.Auth.Commands.CreateCustomer;
using BankSystem.Application.Features.Auth.Commands.DeleteCustomer;
using BankSystem.Application.Features.Auth.Commands.RestoreCustomer;
using BankSystem.Application.Features.Auth.Commands.UpdateCustomer;
using BankSystem.Application.Features.Auth.Queries.GetAccount;
using BankSystem.Application.Features.Auth.Queries.GetCustomer;
using BankSystem.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class CustomerController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet("{customerId:int:min(1)}")]
    [ProducesResponseType(typeof(Result<GetCustomerResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomerById(int customerId)
    {
        var response = await _mediator.Send(new GetCustomerByIdQuery(customerId));
        return Ok(response);
    }

    [HttpGet("by-nationalId/{nationalId}")]
    [ProducesResponseType(typeof(Result<GetCustomerResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomerByNationalId(string nationalId)
    {
        var response = await _mediator.Send(new GetCustomerByNationalIdQuery(nationalId));
        return Ok(response);
    }

    [HttpGet("{customerId:int:min(1)}/accounts")]
    [ProducesResponseType(typeof(Result<GetAccountResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomerByCustomerId(int customerId)
    {
        var response = await _mediator.Send(new GetAccountsByCustomerQuery(customerId));
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CustomerResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateCustomer(CreateCustomerRequestDto request)
    {
        var response = await _mediator.Send(new CreateCustomerCommand(request));
        return Ok(response);
    }

    [HttpPost("{customerId:int:min(1)}/restore")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RestoreCustomer(int customerId)
    {
        var response = await _mediator.Send(new RestoreCustomerCommand(customerId));
        return Ok(response);
    }

    [HttpPut("{customerId:int:min(1)}")]
    [ProducesResponseType(typeof(Result<CustomerResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCustomer(int customerId, UpdateCustomerRequestDto request)
    {
        var response = await _mediator.Send(new UpdateCustomerCommand(customerId, request));
        return Ok(response);
    }

    [HttpDelete("{customerId:int:min(1)}/soft")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> DeleteCustomer(int customerId)
    {
        var response = await _mediator.Send(new SoftDeleteCustomerCommand(customerId));
        return Ok(response);
    }
}
