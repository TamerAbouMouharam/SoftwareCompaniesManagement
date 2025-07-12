using System.ComponentModel.DataAnnotations;

namespace SoftwareCompaniesManagement.Api.DTO.UpdateDto;

public record class UpdateCompanyDto(
    [StringLength(35)]
    string CompanyName,

    [StringLength(500)]
    string Description,

    DateOnly EstablishDate,

    int AccountId
);
