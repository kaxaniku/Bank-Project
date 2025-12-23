using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyBank.API.Models;

namespace MyBank.API.Controllers;

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
    public IActionResult Login([FromBody] UserLoginRequest login)
    {
        IActionResult response = Unauthorized();
        var user = AuthenticateUser(login);

        if (user != null)
        {
            var tokenString = GenerateJwtToken(user);
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

    private UserLogin? AuthenticateUser(UserLoginRequest userLogin)
    {
        if (userLogin.Name == "admin" && userLogin.Password == "admin")
        {
            UserLogin user = new UserLogin
            {
                Name = "admin",
                Password = "admin",
                Role = "Admin"
            };
            return user;
        }
        if (userLogin.Name == "user" && userLogin.Password == "user")
        {
            UserLogin user = new UserLogin
            {
                Name = "user",
                Password = "user",
                Role = "User"
            };
            return user;
        }
        return null;
    }
}