using System.ComponentModel.DataAnnotations;
using SoftwareCompaniesManagement.Model.ValuesCheck;

namespace SoftwareCompaniesManagement.Api.DTO.UpdateDto;

public record class UpdateEmployeeDto(
    int Id,

    [StringLength(30)]
    string FullName,

    [Range(0, double.MaxValue)]
    decimal Salary,

    [AllowedValues("employee", "project_manager", "employee_manager")]
    string Role,

    DateOnly Birthdate,

    DateOnly HiringDate,

    int AccountId,

    int CompanyId
);
