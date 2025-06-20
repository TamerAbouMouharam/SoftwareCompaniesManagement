using System.ComponentModel.DataAnnotations;

namespace SoftwareCompaniesManagement.Api.DTO.CreateDto;

public record class CreateDeveloperDto(
    [Required]
    [StringLength(30)]
    string FullName,
    
    [Required]
    [Range(0, double.MaxValue)]
    decimal Salary,

    DateTime Birthdate,

    DateTime HiringDate,

    [Required]
    int AccountId,

    [Required]
    int CompanyId
);
