using System.ComponentModel.DataAnnotations;
using SoftwareCompaniesManagement.Model.ValuesCheck;

namespace SoftwareCompaniesManagement.Api.DTO.CreateDto;

public record class CreateDeveloperTechnologyDto(
    [Required]
    int DeveloperId,

    [Required]
    int TechnologyId,

    [Required]
    [AllowedValues("freshman", "beginner", "Intermediate", "advanced")]
    string ExperienceLevel,

    [Required]
    double ExperienceYears,

    [Required]
    double Points
);
