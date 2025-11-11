using BankSystem.Application.Common.DTOs.User;
using BankSystem.Application.Common.Interfaces;
using BankSystem.Shared.Models;
using MediatR;

namespace BankSystem.Application.Features.Auth.Commands.RegisterUser;

public record RegisterCommand(string Username, string Email, string Password) : IRequest<Result<RegistrationResponseDto>>;

public class RegisterCommandHandler(IAuthService authService) : IRequestHandler<RegisterCommand, Result<RegistrationResponseDto>>
{
    private readonly IAuthService _authService = authService;

    public async Task<Result<RegistrationResponseDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await _authService.RegisterUserAsync(request.Username, request.Email, request.Password);
    }
}