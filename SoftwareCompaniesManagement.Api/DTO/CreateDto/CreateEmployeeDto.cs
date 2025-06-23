using System.ComponentModel.DataAnnotations;
using SoftwareCompaniesManagement.Model.ValuesCheck;

namespace SoftwareCompaniesManagement.Api.DTO.CreateDto;

public record class CreateEmployeeDto(
    [Required]
    [StringLength(30)]
    string FullName,

    [Required]
    [Range(0, double.MaxValue)]
    decimal Salary,

    [Required]
    [AllowedValues("employee", "project_manager", "employee_manager")]
    string Role,

    [Required]
    DateOnly Birthdate,

    [Required]
    DateOnly HiringDate,

    [Required]
    int AccountId,

    [Required]
    int CompanyId
);
