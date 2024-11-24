using System.ComponentModel.DataAnnotations;

namespace SoftwareCompaniesManagement.Api.DTO.UpdateDto;

public record class UpdateDeveloperProjectDto(
    [Required]
    int DeveloperId,

    [Required]
    int ProjectId,

    [Required]
    double WorkHours
);
