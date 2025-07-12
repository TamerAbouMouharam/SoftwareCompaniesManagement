using System.ComponentModel.DataAnnotations;

namespace SoftwareCompaniesManagement.Api.DTO.UpdateDto;

public record class UpdateDeveloperProjectDto(
    int DeveloperId,

    int ProjectId,

    double WorkHours
);
