using System.ComponentModel.DataAnnotations;

namespace SoftwareCompaniesManagement.Api.DTO.UpdateDto;

public record class UpdateProjectTechnologyDto(
    int ProjectId,

    int TechnologyId
);
