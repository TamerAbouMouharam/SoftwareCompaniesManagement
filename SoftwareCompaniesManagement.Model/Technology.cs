using System.ComponentModel.DataAnnotations;

namespace SoftwareCompaniesManagement.Model;

public class Technology
{
    [Key]
    public int Id { get; set; }

    public required string TechnologyName { get; set; }

    public string? Description { get; set; }
}
