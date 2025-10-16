using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Products.API.Extensions;
internal static class AuthConfig
{
    public static void ConfigureAuth(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "https://iamtheissuer",
                ValidAudience = "https://iamtheaudience",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsTheGreatestKeyEverThatIsLongEnoughToWork"))
            };
        });
    }
}