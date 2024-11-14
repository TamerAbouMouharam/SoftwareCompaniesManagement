using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SoftwareCompaniesManagement.Model;

[PrimaryKey(nameof(DeveloperId), nameof(ProjectId))]
public class DeveloperProject
{
    [ForeignKey("Developer")]
    public int DeveloperId { get; set; }

    [ForeignKey("Project")]
    public int ProjectId { get; set; }

    public Developer Developer { get; set; } = null!;

    public Project Project { get; set; } = null!;

    public double WorkHours { get; set; }
}
