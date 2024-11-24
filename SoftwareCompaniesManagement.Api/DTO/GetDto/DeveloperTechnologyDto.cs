using SoftwareCompaniesManagement.Model.ValuesCheck;

namespace SoftwareCompaniesManagement.Api.DTO.GetDto;

public record class DeveloperTechnologyDto(
    int DeveloperId,

    int TechnologyId,

    ExperienceLevel ExperienceLevel,

    double ExperienceYears,

    double Points
);
