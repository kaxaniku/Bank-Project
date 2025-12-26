using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyBank.API.Models;
using MyBank.API.Models.Requests;
using MyBank.Application.Interfaces.Services;

namespace MyBank.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class LoginController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly ILoginService _loginService;
    private readonly IMapper _mapper;
    public LoginController(IConfiguration config, ILoginService loginService, IMapper mapper)
    {
        _config = config;
        _loginService = loginService;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest login, CancellationToken cancellationToken)
    {
        IActionResult response = Unauthorized();
        var user = await AuthenticateUser(login, cancellationToken);

        if (user != null)
        {
            var tokenString = GenerateJwtToken(user);
            response = Ok(new { Token = tokenString });
        }

        return response;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserLoginRequest userLogin, CancellationToken cancellationToken)
    {
        if (userLogin.Name == null || userLogin.Password == null)
            return BadRequest("Username and password are required.");
        await _loginService.AddUserAsync(userLogin.Name, userLogin.Password, cancellationToken);
        return Ok("User registered successfully.");
    }

    private string GenerateJwtToken(UserLogin login)
    {
        var jwtConfig = _config.GetSection("JwtConfig");

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, login.Username)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: jwtConfig["Issuer"],
            audience: jwtConfig["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<UserLogin?> AuthenticateUser(UserLoginRequest userLogin, CancellationToken cancellationToken)
    {
        if (userLogin.Name == null || userLogin.Password == null)
            return null;
        if(!await _loginService.ValidateUserAsync(userLogin.Name, userLogin.Password, cancellationToken))
            return null;
        var user =  await _loginService.GetLoginByUsernameAsync(userLogin.Name, cancellationToken);
        return _mapper.Map<UserLogin>(user);
    }
}