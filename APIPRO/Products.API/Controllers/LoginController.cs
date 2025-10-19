using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Products.API.Models;

namespace Products.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class LoginController : ControllerBase
{
    private readonly IConfiguration _config;

    public LoginController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLogin login)
    {
        IActionResult response = Unauthorized();
        var user = AuthenticateUser(login);

        if (user != null)
        {
            var tokenString = GenerateJwtToken(login);
            response = Ok(new { Token = tokenString });
        }

        return response;
    }

    private string GenerateJwtToken(UserLogin login)
    {
        var jwtConfig = _config.GetSection("JwtConfig");

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, login.Name),
            new Claim(ClaimTypes.Role, login.Role)
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

    private UserLogin? AuthenticateUser(UserLogin login)
    {
        if (login.Name == "admin" && login.Password == "admin" && login.Role == "Admin")
        {
            return login;
        }
        if (login.Name == "user" && login.Password == "user" && login.Role == "User")
        {
            return login;
        }
        return null;
    }
}