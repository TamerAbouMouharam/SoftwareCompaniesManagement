namespace SoftwareCompaniesManagement.Api.DTO.GetDto;

public record class DeveloperDto(
    int Id,

    string FullName,

    decimal Salary,

    DateTime Birthdate,

    DateTime HiringDate,

    int AccountId,

    int CompanyId
);
