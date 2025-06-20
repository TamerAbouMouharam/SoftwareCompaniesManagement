using System.ComponentModel.DataAnnotations;
using SoftwareCompaniesManagement.Model.ValuesCheck;

namespace SoftwareCompaniesManagement.Api.DTO.UpdateDto;

public record class UpdateDeveloperTechnologyDto(
    [Required]
    int DeveloperId,

    [Required]
    int TechnologyId,

    [Required]
    [AllowedValues("created", "started", "done", "canceled")]
    string ExperienceLevel,

    [Required]
    double ExperienceYears,

    [Required]
    double Points
);
