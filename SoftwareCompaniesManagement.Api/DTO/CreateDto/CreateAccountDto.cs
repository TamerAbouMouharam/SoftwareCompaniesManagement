using System.ComponentModel.DataAnnotations;

namespace SoftwareCompaniesManagement.Api.DTO.CreateDto;

public record class CreateAccountDto(
    [Required]
    [StringLength(20)]
    string Username,

    [Required]
    [Length(8, 50)]
    [RegularExpression("[a-zA-Z0-9]+")]
    string Password
);
