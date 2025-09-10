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
using Microsoft.AspNetCore.Builder;

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
        new Claim("_role", user.Role),
        new Claim("_companyId", user.CompanyId.ToString()),
        new Claim("_isActive", user.IsActive.ToString()),
        new Claim("_infoId", user.InfoId.ToString())
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
        var accountUser = dto.Username;
        return Results.Problem($"[Username: ({accountUser})]", "There is a problem with account", StatusCodes.Status401Unauthorized);
    }

    var token = GenerateToken(user);

    DecodeToken(token).Claims.ToList().ForEach(Console.WriteLine);

    return Results.Ok(new { Token = token, CookieData = new { role = user.Role, companyId = user.CompanyId } });
}).WithParameterValidation();

app.MapPost("signup", (AccountsContext dbContext, SignupDto dto) => 
{
    if(dto is null)
    {
        return Results.BadRequest();
    }

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

    return Results.Ok(new {AccountId = account.Id});
}).WithParameterValidation();

app.MapGet("{companyId}/not_activated", (AccountsContext dbContext, HttpContext httpContext, int companyId) =>
{
    var token = httpContext.Request.Cookies["token"];

    if (token is null)
    {
        return Results.Unauthorized();
    }

    var decodedToken = DecodeToken(token);

    var role = DecodeToken(token).Claims.FirstOrDefault(claim => claim.Type.Equals("_role"))?.Value;
    var tokenCompanyId = DecodeToken(token).Claims.FirstOrDefault(claim => claim.Type.Equals("_companyId"))?.Value;

    Console.WriteLine(role);

    if (role != "company" && role != "employee_manager" || int.Parse(tokenCompanyId) != companyId)
    {
        return Results.Unauthorized();
    }

    var accounts = dbContext.Accounts.Where(account => account.CompanyId == int.Parse(tokenCompanyId)).Where(account => !account.IsActive).ToList();

    return Results.Ok(accounts);
});

app.MapGet("{companyId}/activated", (AccountsContext dbContext, HttpContext httpContext, int companyId) =>
{
    var token = httpContext.Request.Cookies["token"];

    if (token is null)
    {
        return Results.Unauthorized();
    }

    var decodedToken = DecodeToken(token);

    var role = DecodeToken(token).Claims.FirstOrDefault(claim => claim.Type.Equals("_role"))?.Value;
    var tokenCompanyId = DecodeToken(token).Claims.FirstOrDefault(claim => claim.Type.Equals("_companyId"))?.Value;

    Console.WriteLine(role);

    if (role != "company" && role != "employee_manager" || int.Parse(tokenCompanyId) != companyId)
    {
        return Results.Unauthorized();
    }

    var accounts = dbContext.Accounts.Where(account => account.CompanyId == int.Parse(tokenCompanyId)).Where(account => account.IsActive && account.Role != "company").ToList();

    return Results.Ok(accounts);
});

app.MapPut("{accountId}/activate", (AccountsContext dbContext, HttpContext httpContext, int accountId) =>
{
    var token = httpContext.Request.Cookies["token"];

    if (token is null)
    {
        Console.Write("No Token");
        return Results.Unauthorized();
    }

    var accountCompanyId = dbContext.Accounts.Find(accountId).CompanyId;

    var role = DecodeToken(token).FindFirst("_role").Value;
    var companyId = DecodeToken(token).FindFirst("_companyId").Value;

    if (role != "company" && role != "employee_manager" || int.Parse(companyId) != accountCompanyId)
    {
        Console.WriteLine($"{companyId}, {accountCompanyId}");
        return Results.Unauthorized();
    }
    else
    {
        var account = dbContext.Accounts.FirstOrDefault(u => u.Id == accountId);
        account.IsActive = true;
        dbContext.SaveChanges();

        return Results.NoContent();
    }
});

app.MapPut("{accountId}/deactivate", (AccountsContext dbContext, HttpContext httpContext, int accountId) =>
{
    var token = httpContext.Request.Cookies["token"];

    if (token is null)
    {
        Console.Write("No Token");
        return Results.Unauthorized();
    }

    var accountCompanyId = dbContext.Accounts.Find(accountId).CompanyId;

    var role = DecodeToken(token).FindFirst("_role").Value;
    var companyId = DecodeToken(token).FindFirst("_companyId").Value;

    if (role != "company" && role != "employee_manager" || int.Parse(companyId) != accountCompanyId)
    {
        Console.WriteLine($"{companyId}, {accountCompanyId}");
        return Results.Unauthorized();
    }
    else
    {
        var account = dbContext.Accounts.FirstOrDefault(u => u.Id == accountId);
        account.IsActive = false;
        dbContext.SaveChanges();

        return Results.NoContent();
    }
});

app.MapPut("{accountId}/set_company_id/{companyId}", (AccountsContext dbContext, HttpContext httpContext, int accountId, int companyId) =>
{
    var account = dbContext.Accounts.Find(accountId);

    if (account is null)
    {
        return Results.NotFound();
    }

    Account updatedAccount = new Account()
    {
        Id = account.Id,
        Username = account.Username,
        Password = account.Password,
        Role = account.Role,
        CompanyId = companyId,
        IsActive = account.IsActive
    };

    dbContext.Entry(account).CurrentValues.SetValues(updatedAccount);
    dbContext.SaveChanges();

    return Results.NoContent();
});

app.MapPut("{accountId}/set_info_id/{infoId}", (AccountsContext dbContext, HttpContext httpContext, int accountId, int infoId) =>
{
    var account = dbContext.Accounts.Find(accountId);

    if (account is null)
    {
        return Results.NotFound();
    }

    Account updatedAccount = new Account()
    {
        Id = account.Id,
        InfoId = infoId,
        Username = account.Username,
        Password = account.Password,
        Role = account.Role,
        CompanyId = account.CompanyId,
        IsActive = account.IsActive
    };

    dbContext.Entry(account).CurrentValues.SetValues(updatedAccount);
    dbContext.SaveChanges();

    return Results.NoContent();
});

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<AccountsContext>();
dbContext.Database.Migrate();

app.UseCors("AllowedClientCORS");

app.Run();
