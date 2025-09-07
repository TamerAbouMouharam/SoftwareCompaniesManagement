using System.ComponentModel.DataAnnotations;
using SoftwareCompaniesManagement.Model.ValuesCheck;

namespace SoftwareCompaniesManagement.Api.DTO.CreateDto;

public record class CreateTaskDto(
    [Required]
    [StringLength(100)]
    string Title,

    [StringLength(2000)]
    string Description,

    [Required]
    int Priority,

    [Required]
    int Complexity,

    [Required]
    [AllowedValues("created", "started", "done", "canceled", "accepted")]
    string Status,

    [Required]
    DateOnly StartDate,

    [Required]
    DateOnly EndDate,

    [Required]
    double EstimateEffort,

    double ActualEffort,

    int DeveloperId,

    [Required]
    int ProjectId,

    [Required]
    int TechnologyId
);
