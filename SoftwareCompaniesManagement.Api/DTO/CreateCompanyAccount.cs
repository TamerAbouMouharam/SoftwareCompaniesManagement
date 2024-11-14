
namespace SoftwareCompaniesManagement.Api.DTO;

public record CreateCompanyAccount(
    CreateCompanyDto CompanyDto, CreateAccountDto AccountDto
);
