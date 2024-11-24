using System.ComponentModel.DataAnnotations;

namespace SoftwareCompaniesManagement.Api.DTO.CreateDto;

public record class CreateProjectTechnologyDto(
    [Required]
    int ProjectId,

    [Required]
    int TechnologyId
);
