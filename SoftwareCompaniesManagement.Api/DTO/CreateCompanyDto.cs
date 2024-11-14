using System.ComponentModel.DataAnnotations;

namespace SoftwareCompaniesManagement.Api.DTO;

public record class CreateCompanyDto(
    [Required]
    string CompanyName,

    string Description,

    DateOnly EstablishDate,
    
    int AccountId
);