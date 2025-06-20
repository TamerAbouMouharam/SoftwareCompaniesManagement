using System.ComponentModel.DataAnnotations;

namespace SoftwareCompaniesManagement.Api.DTO.UpdateDto;

public record class UpdateCompanyDto(
    [Required]
    [StringLength(35)]
    string CompanyName,

    [StringLength(500)]
    string Description,

    DateTime EstablishDate,

    [Required]
    int AccountId
);
