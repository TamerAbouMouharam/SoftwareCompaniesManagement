using System.ComponentModel.DataAnnotations;
using SoftwareCompaniesManagement.Model.ValuesCheck;

namespace SoftwareCompaniesManagement.Api.DTO.UpdateDto;

public record class UpdateTaskDto(
    [StringLength(100)]
    string Title,

    [StringLength(2000)]
    string Description,

    int Priority,

    int Complexity,

    [AllowedValues("created", "started", "done", "canceled", "accepted")]
    string Status,

    DateOnly StartDate,

    DateOnly EndDate,

    double EstimateEffort,

    double ActualEffort,

    int DeveloperId,

    int ProjectId,

    int TechnologyId
);
