using SoftwareCompaniesManagement.Authorization.Data;
using SoftwareCompaniesManagement.Authorization.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SoftwareCompaniesManagement.Authorization.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("AccountsDatabase");
builder.Services.AddSqlite<AccountsContext>(connectionString);

builder.Services.AddCors(policies => 
{
    policies.AddPolicy("AllowedClientCORS", policy => 
    {
        policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options => 
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "http://localhost:5290",
            ValidAudience = "http://localhost:5173",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("@@E#mqe$$@!!42tEggsWrFFwQrrw$^&#"))
        };
    }
);

var GenerateToken = (Account user) =>
{
    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
        new Claim("role", user.Role),
        new Claim("companyId", user.CompanyId.ToString()),
        new Claim("isActive", user.IsActive.ToString())
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("@@E#mqe$$@!!42tEggsWrFFwQrrw$^&#"));
    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: "http://localhost:5290",
        audience: "http://localhost:5173",
        claims: claims,
        expires: DateTime.UtcNow.AddHours(12),
        signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
};

var DecodeToken = (string token) =>
{
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

    return handler.ValidateToken(token, tokenParameters, out var securityToken);
};

var app = builder.Build();

app.MapPost("login", (AccountsContext dbContext, LoginDto dto) => 
{
    var user = dbContext.Accounts.FirstOrDefault(u => u.Username == dto.Username);

    if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password) || !user.IsActive)
    {
        var accountId = user.Id;
        return Results.Problem($"[Account ID: ({accountId})]", "There is a problem with account", StatusCodes.Status401Unauthorized);
    }

    var token = GenerateToken(user);

    return Results.Ok(new { Token = token, CookieData = new { role = user.Role, companyId = user.CompanyId } });
}).WithParameterValidation();

app.MapPost("signup", (AccountsContext dbContext, SignupDto dto) => 
{
    if (dbContext.Accounts.Any(u => u.Username == dto.Username))
    {
        return Results.BadRequest("Username is already taken.");
    }

    var account = new Account
    {
        Username = dto.Username,
        Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
        Role = dto.Role,
        CompanyId = dto.CompanyId
    };

    account.IsActive = dto.Role == "company" ? true : false;

    dbContext.Accounts.Add(account);
    dbContext.SaveChanges();

    return Results.Ok("Account successfully created!");
}).WithParameterValidation();

app.MapPost("{accountId}/activate", (AccountsContext dbContext, HttpContext httpContext, int accountId) => 
{
    var token = httpContext.Request.Headers.Cookie.FirstOrDefault(cookie => cookie.StartsWith("token")).Split('=').Last();

    var accountCompanyId = dbContext.Accounts.Find(accountId).CompanyId;

    var role = DecodeToken(token).FindFirst("role").Value;
    var companyId = DecodeToken(token).FindFirst("companyId").Value;

    if(token is null || role != "company" && role != "employee_manager" || int.Parse(companyId) != accountCompanyId)
    {
        return Results.Unauthorized();
    }
    else
    {
        var account = dbContext.Accounts.FirstOrDefault(u => u.Id == accountId);
        account.IsActive = true;
        dbContext.SaveChanges();

        return Results.Ok("Account successfully activated!");
    }
});

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<AccountsContext>();
dbContext.Database.Migrate();

app.UseCors("AllowedClientCORS");

app.Run();
