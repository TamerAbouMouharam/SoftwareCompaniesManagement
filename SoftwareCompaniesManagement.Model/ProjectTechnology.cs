using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SoftwareCompaniesManagement.Model;

[PrimaryKey(nameof(ProjectId), nameof(TechnologyId))]
public class ProjectTechnology
{
    [ForeignKey("Project")]
    public int ProjectId { get; set; }

    [ForeignKey("Technology")]
    public int TechnologyId { get; set; }

    public Project Project { get; set; } = null!;

    public Technology Technology { get; set; } = null!;
}
