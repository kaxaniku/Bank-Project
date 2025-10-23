using BankSystem.Application.Common.DTOs;

namespace BankSystem.Application.Common.Interfaces;

public interface IJwtService
{
    string GenerateJwtToken(UserDto user);
}
