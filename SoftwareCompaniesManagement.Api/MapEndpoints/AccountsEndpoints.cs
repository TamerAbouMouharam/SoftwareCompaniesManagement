using SoftwareCompaniesManagement.Api.DTO.GetDto;
using SoftwareCompaniesManagement.Api.DTO.CreateDto;
using SoftwareCompaniesManagement.Api.DTO.UpdateDto;
using SoftwareCompaniesManagement.Api.Data;
using SoftwareCompaniesManagement.Model;
using SoftwareCompaniesManagement.Model.ValuesCheck;
using AutoMapper;
using SoftwareCompaniesManagement.Api.DTO;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Builder;
using System.Runtime.InteropServices;

namespace SoftwareCompaniesManagement.Api.MapEndpoints;

public static class AccountsEndpoints
{
    public static RouteGroupBuilder MapAccountsEndpoints(this WebApplication app)
    {
        var mappingConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Account, AccountDto>();
            cfg.CreateMap<CreateAccountDto, Account>();
        });

        var accountMapper = mappingConfiguration.CreateMapper();

        var accountsGroup = app.MapGroup("accounts").WithParameterValidation();

        accountsGroup.MapGet("{id}", (int id, CompaniesContext dbContext) =>
        {
            var account = dbContext.Accounts.FirstOrDefault(account => account.Id == id);

            return accountMapper.Map<Account, AccountDto>(account);

        }).WithName("GetAccount");

        accountsGroup.MapPost("", (CreateAccountDto accountDto, CompaniesContext dbContext) =>
        {
            var account = accountMapper.Map<CreateAccountDto, Account>(accountDto);

            dbContext.Accounts.Add(account);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute("GetAccount", new { id = account.Id }, accountMapper.Map<Account, AccountDto>(account));
        });

        accountsGroup.MapDelete("{accountId}", (HttpContext httpContext, CompaniesContext dbContext, int accountId) =>
        {

        });

        return accountsGroup;
    }
}
