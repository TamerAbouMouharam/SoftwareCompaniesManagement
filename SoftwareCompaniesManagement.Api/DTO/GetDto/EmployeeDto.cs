using SoftwareCompaniesManagement.Model.ValuesCheck;

namespace SoftwareCompaniesManagement.Api.DTO.GetDto;

public record class EmployeeDto(
    int Id,

    string FullName,

    decimal Salary,

    string Role,

    DateTime Birthdate,

    DateTime HiringDate,

    int AccountId,

    int CompanyId
);
