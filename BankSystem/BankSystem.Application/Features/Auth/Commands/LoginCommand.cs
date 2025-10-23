using BankSystem.Application.Common.DTOs;
using BankSystem.Application.Common.Interfaces;
using BankSystem.Shared.Models;
using MediatR;

namespace BankSystem.Application.Features.Auth.Commands;

public record LoginCommand(string Username, string Password) : IRequest<Result<LoginResponseDto>>;

public class LoginHandler(IAuthService authService) : IRequestHandler<LoginCommand, Result<LoginResponseDto>>
{
    private readonly IAuthService _authService = authService;

    public async Task<Result<LoginResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await _authService.LoginUserAsync(request.Username, request.Password);
    }
}