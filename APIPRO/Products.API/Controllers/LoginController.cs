using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Products.API.Models;

namespace Products.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class LoginController : ControllerBase
{
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
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, login.Name),
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsTheGreatestKeyEverThatIsLongEnoughToWork"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: "https://iamtheissuer",
            audience: "https://iamtheaudience",
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private UserLogin? AuthenticateUser(UserLogin login)
    {
        if (login.Name == "admin" && login.Password == "admin")
        {
            return login;
        }
        return null;
    }
}