using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace SoftwareCompaniesManagement.Api.MapEndpoints;

public static class TokenDecoder
{
    public static ClaimsPrincipal DecodeToken(HttpContext context)
    {
        var authCookie = context.Request.Cookies["token"];

        if(authCookie is null)
        {
            return null;
        }

        var handler = new JwtSecurityTokenHandler();

        var tokenParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "http://localhost:5290",
            ValidateAudience = true,
            ValidAudience = "http://localhost:5173",
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("@@E#mqe$$@!!42tEggsWrFFwQrrw$^&#"))
        };

        var token = handler.ValidateToken(authCookie, tokenParameters, out _);

        if (token is null)
        {
            return null;
        }

        return token;
    }
}
