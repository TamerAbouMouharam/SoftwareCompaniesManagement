using System.ComponentModel.DataAnnotations;
using SoftwareCompaniesManagement.Model.ValuesCheck;

namespace SoftwareCompaniesManagement.Api.DTO.CreateDto;

public record class CreateDeveloperTechnologyDto(
    [Required]
    int DeveloperId,

    [Required]
    int TechnologyId,

    [Required]
    ExperienceLevel ExperienceLevel,

    [Required]
    double ExperienceYears,

    [Required]
    double Points
);
