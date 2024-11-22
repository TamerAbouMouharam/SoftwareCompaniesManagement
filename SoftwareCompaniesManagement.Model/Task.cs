using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoftwareCompaniesManagement.Model.ValuesCheck;

namespace SoftwareCompaniesManagement.Model;

public class Task
{
    [Key]
    public int Id { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public int Priority { get; set; }

    public int Complexity { get; set; }

    public required Status Status { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public double EstimateEffort { get; set; }

    public double? ActualEffort { get; set; }

    [ForeignKey("Developer")]
    public int? DeveloperId { get; set; }

    public Developer? Developer { get; set; }

    [ForeignKey("Project")]
    public int ProjectId { get; set; }

    public Project Project { get; set; } = null!;

    [ForeignKey("Technology")]
    public int TechnologyId { get; set; }

    public Technology Technology { get; set; } = null!;
}
