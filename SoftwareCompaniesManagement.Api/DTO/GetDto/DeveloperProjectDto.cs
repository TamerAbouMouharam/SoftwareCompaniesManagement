namespace SoftwareCompaniesManagement.Api.DTO.GetDto;

public record class DeveloperProjectDto(
    int DeveloperId,

    int ProjectId,

    double WorkHours
);
