using System.ComponentModel.DataAnnotations;
using SoftwareCompaniesManagement.Model.ValuesCheck;

namespace SoftwareCompaniesManagement.Api.DTO.UpdateDto;

public record class UpdateDeveloperTechnologyDto(
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
