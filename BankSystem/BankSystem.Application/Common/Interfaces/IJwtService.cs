using BankSystem.Application.Common.DTOs.User;

namespace BankSystem.Application.Common.Interfaces;

public interface IJwtService
{
    string GenerateJwtToken(UserDto user);
}
