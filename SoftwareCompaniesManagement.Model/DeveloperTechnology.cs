using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SoftwareCompaniesManagement.Model.ValuesCheck;

namespace SoftwareCompaniesManagement.Model;

[PrimaryKey(nameof(DeveloperId), nameof(TechnologyId))]
public class DeveloperTechnology
{
    [ForeignKey("Developer")]
    public int DeveloperId { get; set; }

    [ForeignKey("Technology")]
    public int TechnologyId { get; set; }

    public Developer Developer { get; set; } = null!;

    public Technology Technology { get; set; } = null!;

    public required ExperienceLevel ExperienceLevel { get; set; }

    public double ExperienceYears { get; set; }

    public double Points { get; set; }
}
