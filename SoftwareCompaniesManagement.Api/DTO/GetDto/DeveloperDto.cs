namespace SoftwareCompaniesManagement.Api.DTO.GetDto;

public record class DeveloperDto(
    int Id,

    string FullName,

    decimal Salary,

    DateOnly Birthdate,

    DateOnly HiringDate,

    int AccountId,

    int CompanyId
);
