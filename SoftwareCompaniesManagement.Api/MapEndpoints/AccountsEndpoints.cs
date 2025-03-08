using SoftwareCompaniesManagement.Api.DTO.GetDto;
using SoftwareCompaniesManagement.Api.DTO.CreateDto;
using SoftwareCompaniesManagement.Api.DTO.UpdateDto;
using SoftwareCompaniesManagement.Api.Data;
using SoftwareCompaniesManagement.Model;
using AutoMapper;

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

        accountsGroup.MapPost("", (CompaniesContext dbContext, CreateAccountDto accountDto) => 
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

        return accountsGroup;
    }
}
