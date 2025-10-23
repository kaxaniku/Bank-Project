using BankSystem.Application.Common.DTOs;
using System.Security.Claims;

namespace BankSystem.Application.Common.Interfaces;

public interface IJwtService
{
    string GenerateJwtToken(UserDto user);
}
