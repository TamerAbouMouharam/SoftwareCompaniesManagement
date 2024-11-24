using System.ComponentModel.DataAnnotations;
using SoftwareCompaniesManagement.Model.ValuesCheck;

namespace SoftwareCompaniesManagement.Api.DTO.UpdateDto;

public record class UpdateTaskDto(
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
    Status Status,

    [Required]
    DateOnly StartDate,

    [Required]
    DateOnly EndDate,

    [Required]
    double EstimateEffort,

    double ActualEffort,

    [Required]
    int DeveloperId,

    [Required]
    int ProjectId,

    [Required]
    int TechnologyId
);
