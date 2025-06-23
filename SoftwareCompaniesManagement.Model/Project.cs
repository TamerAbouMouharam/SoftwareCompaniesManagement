using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoftwareCompaniesManagement.Model.ValuesCheck;

namespace SoftwareCompaniesManagement.Model;

public class Project
{
    [Key]
    public int Id { get; set; }

    public required string ProjectName { get; set; }

    public string? Description { get; set; }

    public int ProjectPoints { get; set; }

    [AllowedValues("created", "started", "done", "canceled")]
    public required string Status { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    [ForeignKey("Manager")]
    public int ManagerId { get; set; }

    public Employee Manager { get; set; } = null!;

    [ForeignKey("Company")]
    public int CompanyId { get; set; }

    public Company Company { get; set; } = null!;

    public ICollection<ProjectTechnology> ProjectTechnologies { get; set; } = null!;

    public ICollection<Task> Tasks { get; set; } = null!;

    public ICollection<DeveloperProject> DeveloperProjects { get; set; } = null!;
}
