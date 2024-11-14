using SoftwareCompaniesManagement.Api.DTO;
using SoftwareCompaniesManagement.Model;

namespace SoftwareCompaniesManagement.Api.EntityDtoMapping;

public static class AccountMapping
{
    public static CreateAccountDto ToDto(this Account account)
    {
        return new CreateAccountDto(
            account.Username,
            account.Password
        );
    }

    public static Account ToEntity(this CreateAccountDto accountDto)
    {
        return new Account() {
            Username = accountDto.Username,
            Password = accountDto.Password
        };
    }
}
