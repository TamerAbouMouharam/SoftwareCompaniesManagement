using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareCompaniesManagement.Model;

public class Company
{
    [Key]
    public int Id { get; set; }

    public required string CompanyName { get; set; }

    public string? Description { get; set; }

    public DateOnly? EstablishDate { get; set; }

    [ForeignKey("Account")]
    public int AccountId { get; set; }

    public Account Account { get; set; } = null!;

    public ICollection<Employee> Employees { get; set; } = null!;

    public ICollection<Developer> Developers { get; set; } = null!;

    public ICollection<Project>? Projects { get; set; }
}
