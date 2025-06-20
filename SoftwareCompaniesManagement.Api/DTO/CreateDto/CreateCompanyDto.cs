using System.ComponentModel.DataAnnotations;

namespace SoftwareCompaniesManagement.Api.DTO.CreateDto;

public record class CreateCompanyDto(
    [Required]
    [StringLength(35)]
    string CompanyName,

    [StringLength(500)]
    string Description,

    DateTime EstablishDate,

    [Required]
    int AccountId
);