using System.ComponentModel.DataAnnotations;

namespace SoftwareCompaniesManagement.Api.DTO.CreateDto;

public record class CreateTechnologyDto(
    [Required]
    [StringLength(30)]
    string TechnologyName,

    [StringLength(500)]
    string Description
);
