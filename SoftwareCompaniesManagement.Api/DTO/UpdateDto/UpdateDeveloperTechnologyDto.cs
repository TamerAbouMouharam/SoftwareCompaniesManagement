using System.ComponentModel.DataAnnotations;
using SoftwareCompaniesManagement.Model.ValuesCheck;

namespace SoftwareCompaniesManagement.Api.DTO.UpdateDto;

public record class UpdateDeveloperTechnologyDto(
    int DeveloperId,

    int TechnologyId,

    [AllowedValues("freshman", "beginner", "intermediate", "advanced")]
    string ExperienceLevel,

    double ExperienceYears,

    double Points
);
