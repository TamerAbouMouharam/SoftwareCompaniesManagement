using System.ComponentModel.DataAnnotations;

namespace SoftwareCompaniesManagement.Api.DTO.CreateDto;

public record class CreateDeveloperProjectDto(
    [Required]
    int DeveloperId,

    [Required]
    int ProjectId,

    [Required]
    double WorkHours
);
