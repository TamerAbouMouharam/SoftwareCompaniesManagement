namespace SoftwareCompaniesManagement.Api.DTO.GetDto;

public record class CompanyDto(
    int Id,

    string CompanyName,

    string Description,

    DateOnly EstablishDate,

    int AccountId
);
