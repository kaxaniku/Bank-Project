using AutoMapper;
using BankSystem.Application.Common.DTOs.User;
using BankSystem.Application.Common.Interfaces;
using BankSystem.Shared.Models;
using Microsoft.AspNetCore.Identity;

namespace BankSystem.Infrastructure.Identity;

public class AuthService(UserManager<IdentityUser> userManager, IJwtService jwtService, IMapper mapper) : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly IJwtService _jwtService = jwtService;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<RegistrationResponseDto>> RegisterUserAsync(string username, string email, string password)
    {
        var existingUser = await _userManager.FindByNameAsync(username);
        existingUser ??= await _userManager.FindByEmailAsync(email);

        if (existingUser != null)
        {
            return BuildFailureResult<RegistrationResponseDto>(409, ["User already exists."]);
        }

        return await CreateUserAsync(username, email, password);
    }

    public async Task<Result<LoginResponseDto>> LoginUserAsync(string username, string password)
    {
        var validationResult = await ValidateUserCredentialsAsync(username, password);

        if (!validationResult.Succeeded)
            return new Result<LoginResponseDto>
            {
                Code = validationResult.Code,
                Succeeded = false,
                Messages = validationResult.Messages
            };

        return await HandleLoginSuccessAsync(validationResult.Data);
    }

    private async Task<Result<IdentityUser>> ValidateUserCredentialsAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
            return BuildFailureResult<IdentityUser>(401, ["Invalid credentials."]);

        if (await _userManager.IsLockedOutAsync(user))
            return BuildFailureResult<IdentityUser>(403, ["Your account is locked due to multiple failed login attempts."]);

        if (!await _userManager.CheckPasswordAsync(user, password))
        {
            await HandleFailedLoginAttemptAsync(user);
            return BuildFailureResult<IdentityUser>(401, ["Invalid credentials."]);
        }

        return new Result<IdentityUser> { Succeeded = true, Data = user };
    }

    private async Task<Result<LoginResponseDto>> HandleLoginSuccessAsync(IdentityUser user)
    {
        if (await _userManager.GetAccessFailedCountAsync(user) > 0)
            await _userManager.ResetAccessFailedCountAsync(user);

        var userDto = _mapper.Map<UserDto>(user);
        var token = _jwtService.GenerateJwtToken(userDto);

        await _userManager.UpdateAsync(user);

        return new Result<LoginResponseDto>
        {
            Code = 200,
            Succeeded = true,
            Data = new LoginResponseDto
            {
                AccessToken = token
            },
            Messages = ["Login successful"]
        };
    }

    private async Task HandleFailedLoginAttemptAsync(IdentityUser user)
    {
        await _userManager.AccessFailedAsync(user);
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
            return BuildFailureResult<RegistrationResponseDto>(400, [.. result.Errors.Select(e => $"{e.Code}: {e.Description}")]);
        }

        return await BuildSuccessResultAsync(user);
    }

    private static Task<Result<RegistrationResponseDto>> BuildSuccessResultAsync(IdentityUser user)
    {
        return Task.FromResult(new Result<RegistrationResponseDto>
        {
            Succeeded = true,
            Code = 201,
            Data = new RegistrationResponseDto { UserId = user.Id },
            Messages = ["User registered successfully"]
        });
    }

    private static Result<T> BuildFailureResult<T>(int code, List<string> messages)
    {
        return new Result<T>
        {
            Succeeded = false,
            Code = code,
            Messages = messages
        };
    }
}
