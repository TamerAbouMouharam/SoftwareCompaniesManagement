using SoftwareCompaniesManagement.Api.DTO;
using SoftwareCompaniesManagement.Model;

namespace SoftwareCompaniesManagement.Api.EntityDtoMapping;

public static class CompanyMapping
{
    public static Company ToEntity(this CreateCompanyDto companyDto)
    {
        return new Company {
            CompanyName = companyDto.CompanyName,
            AccountId = companyDto.AccountId,
            Description = companyDto.Description,
            EstablishDate = companyDto.EstablishDate
        };
    }
}
