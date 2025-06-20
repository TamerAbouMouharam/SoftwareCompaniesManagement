using System.ComponentModel.DataAnnotations;

namespace SoftwareCompaniesManagement.Authorization.DTO;

public record class SignupDto(
    [Required]
    [StringLength(20)]
    string Username,

    [Required]
    [Length(8, 50)]
    [RegularExpression("[a-zA-Z0-9]+")]
    string Password,

    [AllowedValues("company", "employee_manager", "project_manager", "developer", "employee")]
    string Role,

    int CompanyId
);
