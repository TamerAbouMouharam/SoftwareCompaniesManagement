using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareCompaniesManagement.Model;

public class Developer
{
    [Key]
    public int Id { get; set; }

    public required string FullName { get; set; }

    public decimal Salary { get; set; }

    public DateTime Birthdate { get; set; }

    public DateTime HiringDate { get; set; }

    [ForeignKey("Account")]
    public int AccountId { get; set; }

    public Account Account { get; set; } = null!;

    [ForeignKey("Company")]
    public int CompanyId { get; set; }

    public Company Company { get; set; } = null!;

    public ICollection<Task>? Tasks { get; set; }

    public ICollection<DeveloperTechnology> DeveloperTechnologies { get; set; } = null!;

    public ICollection<DeveloperProject>? DeveloperProjects { get; set; }
}
