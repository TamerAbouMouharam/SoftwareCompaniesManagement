using System.ComponentModel.DataAnnotations;

namespace SoftwareCompaniesManagement.Api.DTO.UpdateDto;

public record class UpdateTechnologyDto(
    [Required]
    [StringLength(30)]
    string TechnologyName,

    [StringLength(500)]
    string Description
);
