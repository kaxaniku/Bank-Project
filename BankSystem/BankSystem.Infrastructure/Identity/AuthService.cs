using BankSystem.Application.Common.DTOs;
using BankSystem.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using BankSystem.Shared.Models;

namespace BankSystem.Infrastructure.Identity;

public class AuthService(UserManager<IdentityUser> userManager) : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager = userManager;

    public async Task<Result<RegistrationResponseDto>> RegisterUserAsync(string username, string email, string password)
    {
        var existingUser = await _userManager.FindByNameAsync(username);

        if (existingUser != null)
        {
            return new Result<RegistrationResponseDto>
            {
                Succeeded = false,
                Code = 409,
                Messages = { "User already exists." }
            };
        }

        return await CreateUserAsync(username, email, password);
    }

    private async Task<Result<RegistrationResponseDto>> CreateUserAsync(string username, string email, string password)
    {
        var user = new IdentityUser
        {
            UserName = username,
            Email = email
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return new Result<RegistrationResponseDto>
            {
                Succeeded = false,
                Code = 400,
                Messages = [.. result.Errors.Select(e => $"{e.Code}: {e.Description}")]
            };
        }

        return await BuildRegistrationResultAsync(user);
    }

    private static Task<Result<RegistrationResponseDto>> BuildRegistrationResultAsync(IdentityUser user)
    {
        return Task.FromResult(new Result<RegistrationResponseDto>
        {
            Succeeded = true,
            Code = 201,
            Data = new RegistrationResponseDto { UserId = user.Id },
            Messages = ["User registered successfully"]
        });
    }
}
