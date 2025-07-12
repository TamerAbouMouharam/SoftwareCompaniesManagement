using System.ComponentModel.DataAnnotations;
using SoftwareCompaniesManagement.Model.ValuesCheck;

namespace SoftwareCompaniesManagement.Api.DTO.UpdateDto;

public record class UpdateProjectDto(
    [StringLength(100)]
    string ProjectName,

    [StringLength(2000)]
    string Description,

    int ProjectPoints,

    [AllowedValues("created", "started", "done", "canceled")]
    string Status,

    DateOnly StartDate,

    DateOnly EndDate,

    int ManagerId,

    int CompanyId
);
