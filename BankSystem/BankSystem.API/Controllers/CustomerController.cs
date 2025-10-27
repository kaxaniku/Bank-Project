using BankSystem.Application.Common.DTOs;
using BankSystem.Application.Features.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost()]
    public async Task<IActionResult> Create(CreateCustomerRequestDto request)
    {
        var response = await _mediator.Send(new CreateCustomerCommand(request));
        return Ok(response);
    }

}
