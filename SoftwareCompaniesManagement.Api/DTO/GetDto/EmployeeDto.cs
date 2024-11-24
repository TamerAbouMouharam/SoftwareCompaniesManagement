using SoftwareCompaniesManagement.Model.ValuesCheck;

namespace SoftwareCompaniesManagement.Api.DTO.GetDto;

public record class EmployeeDto(
    int Id,

    string FullName,

    decimal Salary,

    Role Role,

    DateOnly Birthdate,

    DateOnly HiringDate,

    int AccountId,

    int CompanyId
);
