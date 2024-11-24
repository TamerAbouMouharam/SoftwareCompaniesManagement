using System.ComponentModel.DataAnnotations;
using SoftwareCompaniesManagement.Model.ValuesCheck;

namespace SoftwareCompaniesManagement.Api.DTO.UpdateDto;

public record class UpdateProjectDto(
    [Required]
    [StringLength(100)]
    string ProjectName,

    [StringLength(2000)]
    string Description,

    [Required]
    int ProjectPoints,

    [Required]
    Status Status,

    [Required]
    DateOnly StartDate,

    [Required]
    DateOnly EndDate,

    [Required]
    int ManagerId,

    [Required]
    int CompanyId
);
