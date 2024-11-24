using System.ComponentModel.DataAnnotations;
using SoftwareCompaniesManagement.Model.ValuesCheck;

namespace SoftwareCompaniesManagement.Api.DTO.UpdateDto;

public record class UpdateEmployeeDto(
    [Required]
    int Id,

    [Required]
    [StringLength(30)]
    string FullName,

    [Required]
    [Range(0, double.MaxValue)]
    decimal Salary,

    [Required]
    Role Role,

    [Required]
    DateOnly Birthdate,

    [Required]
    DateOnly HiringDate,

    [Required]
    int AccountId,

    [Required]
    int CompanyId
);
