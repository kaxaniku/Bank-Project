using BankSystem.Application.Common.DTOs;
using BankSystem.Shared.Models;

namespace BankSystem.Application.Common.Interfaces;

public interface IAuthService
{
    Task<Result<RegistrationResponseDto>> RegisterUserAsync(string username, string email, string password);
    Task<Result<LoginResponseDto>> LoginUserAsync(string username, string password);
}
