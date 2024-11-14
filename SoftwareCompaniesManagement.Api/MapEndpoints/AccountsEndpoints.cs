using SoftwareCompaniesManagement.Api.DTO;
using SoftwareCompaniesManagement.Api.EntityDtoMapping;
using SoftwareCompaniesManagement.Api.Data;

namespace SoftwareCompaniesManagement.Api.MapEndpoints;

public static class AccountsEndpoints
{
    public static void MapAccountsEndpoints(this WebApplication app)
    {
        app.MapPost("/Accounts/CompanyAccount", (CompaniesContext dbContext, CreateCompanyAccount companyAccount) => {
            var accountEntity = companyAccount.AccountDto.ToEntity();
            dbContext.Accounts.Add(accountEntity);
            dbContext.SaveChanges();
        });
    }
}
