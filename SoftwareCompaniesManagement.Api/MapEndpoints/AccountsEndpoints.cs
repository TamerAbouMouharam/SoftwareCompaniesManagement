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
        });

        return accountsGroup;
    }
}
