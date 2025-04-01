using SoftwareCompaniesManagement.Api.DTO.GetDto;
using SoftwareCompaniesManagement.Api.DTO.CreateDto;
using SoftwareCompaniesManagement.Api.DTO.UpdateDto;
using SoftwareCompaniesManagement.Api.Data;
using SoftwareCompaniesManagement.Model;
using SoftwareCompaniesManagement.Model.ValuesCheck;
using AutoMapper;
using SoftwareCompaniesManagement.Api.DTO;
using Microsoft.AspNetCore.DataProtection;

namespace SoftwareCompaniesManagement.Api.MapEndpoints;

public static class AccountsEndpoints
{
    public static RouteGroupBuilder MapAccountsEndpoints(this WebApplication app)
    {
        var mapping_configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Account, AccountDto>();
            cfg.CreateMap<CreateAccountDto, Account>();
        });

        var account_mapper = mapping_configuration.CreateMapper();

        var accountsGroup = app.MapGroup("accounts").WithParameterValidation();

        accountsGroup.MapGet("{id}", (CompaniesContext dbContext, int id) => 
        {
            var account = dbContext.Accounts.Find(id);

            if(account is null) 
            {
                return Results.NotFound("This account does not exist");
            }
            else
            {
                return Results.Ok(account_mapper.Map<AccountDto>(account));
            }
        }).WithName("accountGET");

        accountsGroup.MapPost("create", (CompaniesContext dbContext, CreateAccountDto accountDto) => 
        {
            var account = account_mapper.Map<Account>(accountDto);

            if(dbContext.Accounts.Count(account_ => account_.Username == account.Username) == 0)
            {
                dbContext.Accounts.Add(account);
                dbContext.SaveChanges();

                return Results.CreatedAtRoute("accountGET", new { id = account.Id }, account_mapper.Map<AccountDto>(account));
            }
            else
            {
                return Results.BadRequest("Account already exists");
            }
        });

        accountsGroup.MapPost("login", (HttpContext httpContext, CompaniesContext dbContext, IDataProtectionProvider protectionProvider, LoginDto login) => 
        {
            var account = dbContext.Accounts.Where(account_ => account_.Username == login.Username && account_.Password == login.Password).FirstOrDefault();

            if(account is null)
            {
                return Results.BadRequest("Wrong username or password.");
            }
            else
            {
                var username = account.Username;
                var password = account.Password;

                var protector = protectionProvider.CreateProtector("Authentication");

                var role = "";

                if(dbContext.Employees.Where(employee_ => employee_.AccountId == account.Id) is ICollection<Employee> employee && employee.Count() > 0)
                {
                    switch(employee.First().Role)
                    {
                        case Role.Employee:
                            role = "employee";
                            break;
                        case Role.EmployeesManager:
                            role = "employeemanager";
                            break;
                        case Role.ProjectManager:
                            role = "projectmanager";
                            break;
                        default:
                            goto case Role.Employee;
                    }
                }
                else
                {
                    if(dbContext.Companies.Where(company_ => company_.AccountId == account.Id).Count() > 0)
                    {
                        role = "company";
                    }

                    if(dbContext.Developers.Where(developer_ => developer_.AccountId == account.Id).Count() > 0)
                    {
                        role = "developer";
                    }
                }

                var authCookiePayload = $"username:{username};password:{password};role:{role};login:1";
                authCookiePayload = protector.Protect(authCookiePayload);

                return Results.Ok(authCookiePayload);
            }
        });

        return accountsGroup;
    }
}
