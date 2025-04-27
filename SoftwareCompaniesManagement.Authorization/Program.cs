using SoftwareCompaniesManagement.Authorization.Data;
using SoftwareCompaniesManagement.Authorization.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SoftwareCompaniesManagement.Authorization.DTO;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("AccountsDatabase");
builder.Services.AddSqlite<AccountsContext>(connectionString);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options => 
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "YourIssuer",
            ValidAudience = "YourAudience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKey"))
        };
    }
);

var GenerateToken = (Account user) =>
{
    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
        new Claim("role", user.Role),
        new Claim("companyId", user.CompanyId.ToString())
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKey"));
    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: "http://localhost:5290",
        audience: "http://localhost:5173",
        claims: claims,
        expires: DateTime.UtcNow.AddHours(1),
        signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
};

var app = builder.Build();

app.MapPost("login", (AccountsContext dbContext, LoginDto dto) => 
{
    var user = dbContext.Accounts.FirstOrDefault(u => u.Username == dto.Username);

    if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password) || !user.IsActive)
    {
        return Results.Unauthorized();
    }

    var token = GenerateToken(user);
    return Results.Ok(new { Token = token });
});

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
        CompanyId = dto.CompanyId,
    };

    account.IsActive = false;

    dbContext.Accounts.Add(account);
    dbContext.SaveChanges();

    return Results.Ok("Account successfully created!");
});

app.Run();
