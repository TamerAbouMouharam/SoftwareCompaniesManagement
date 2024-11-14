using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareCompaniesManagement.Model;

public class Employee
{
    [Key]
    public int Id { get; set; }

    public required string FullName { get; set; }

    public decimal Salary { get; set; }

    public required string Role { get; set; }

    public DateOnly Birthdate { get; set; }

    public DateOnly HiringDate { get; set; }
    
    [ForeignKey("Account")]
    public int AccountId { get; set; }

    public Account Account { get; set; } = null!;

    [ForeignKey("Company")]
    public int CompanyId { get; set; }

    public Company Company { get; set; } = null!;

    public ICollection<Project>? Projects { get; set; }
}
