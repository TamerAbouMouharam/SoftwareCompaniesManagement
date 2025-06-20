using SoftwareCompaniesManagement.Model.ValuesCheck;

namespace SoftwareCompaniesManagement.Api.DTO.GetDto;

public record class ProjectDto(
    int Id,

    string ProjectName,

    string Description,

    int ProjectPoints,

    string Status,

    DateTime StartDate,

    DateTime EndDate,

    int ManagerId,

    int CompanyId
);
