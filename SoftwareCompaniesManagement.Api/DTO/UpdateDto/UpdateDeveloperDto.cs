using System.ComponentModel.DataAnnotations;

namespace SoftwareCompaniesManagement.Api.DTO.UpdateDto;

public record class UpdateDeveloperDto(
    [StringLength(30)]
    string FullName,
    
    [Range(0, double.MaxValue)]
    decimal Salary,

    DateOnly Birthdate,

    DateOnly HiringDate,

    int AccountId,

    int CompanyId
);
