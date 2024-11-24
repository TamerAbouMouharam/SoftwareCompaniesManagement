using System.ComponentModel.DataAnnotations;

namespace SoftwareCompaniesManagement.Api.DTO.UpdateDto;

public record class UpdateProjectTechnologyDto(
    [Required]
    int ProjectId,

    [Required]
    int TechnologyId
);
