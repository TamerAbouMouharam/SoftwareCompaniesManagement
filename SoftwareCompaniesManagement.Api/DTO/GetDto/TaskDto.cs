using SoftwareCompaniesManagement.Model.ValuesCheck;

namespace SoftwareCompaniesManagement.Api.DTO.GetDto;

public record class TaskDto(
    int Id,

    string Title,

    string Description,

    int Priority,

    int Complexity,

    string Status,

    DateTime StartDate,

    DateTime EndDate,

    double EstimateEffort,

    double ActualEffort,

    int DeveloperId,

    int ProjectId,

    int TechnologyId
);
