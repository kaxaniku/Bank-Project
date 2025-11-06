using BankSystem.Application.Common.DTOs;
using BankSystem.Application.Features.Auth.Commands.CreateCustomer;
using BankSystem.Application.Features.Auth.Commands.DeleteCustomer;
using BankSystem.Application.Features.Auth.Commands.GetCustomer;
using BankSystem.Application.Features.Auth.Commands.RestoreCustomer;
using BankSystem.Application.Features.Auth.Commands.UpdateCustomer;
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomerById(int customerId)
    {
        var response = await _mediator.Send(new GetCustomerByIdCommand(customerId));
        return Ok(response);
    }

    [HttpGet("by/{nationalId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomerByNationalId(string nationalId)
    {
        var response = await _mediator.Send(new GetCustomerByNationalIdCommand(nationalId));
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateCustomer(int customerId, UpdateCustomerRequestDto request)
    {
        var response = await _mediator.Send(new UpdateCustomerCommand(customerId, request));
        return Ok(response);
    }

    [HttpDelete("{customerId:int:min(1)}/soft")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> DeleteCustomer(int customerId)
    {
        var response = await _mediator.Send(new SoftDeleteCustomerCommand(customerId));
        return Ok(response);
    }

    [HttpDelete("{customerId:int:min(1)}/permanent")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> PermanentlyDeleteCustomer(int customerId)
    {
        var response = await _mediator.Send(new DeleteCustomerCommand(customerId));
        return Ok(response);
    }
}
