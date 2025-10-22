using BankSystem.Application.Common.DTOs;
using BankSystem.Application.Features.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register([FromBody] RegistrationRequestDto request)
    {
        var response = await _mediator.Send(new RegisterCommand(request.Username, request.Email, request.Password));
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] RegistrationRequestDto request)
    {
        throw new NotImplementedException();
        //var response = await _mediator.Send(new RegisterCommand(request.Username, request.Email, request.Password));
        //return Ok(response);
    }
}
