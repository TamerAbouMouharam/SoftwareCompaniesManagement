using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoftwareCompaniesManagement.Model.ValuesCheck;

namespace SoftwareCompaniesManagement.Model;

public class Employee
{
    [Key]
    public int Id { get; set; }

    public required string FullName { get; set; }

    public decimal Salary { get; set; }

    [AllowedValues("employee", "project_manager", "employee_manager")]
    public required string Role { get; set; }

    public DateTime Birthdate { get; set; }

    public DateTime HiringDate { get; set; }
    
    [ForeignKey("Account")]
    public int AccountId { get; set; }

    public Account Account { get; set; } = null!;

    [ForeignKey("Company")]
    public int CompanyId { get; set; }

    public Company Company { get; set; } = null!;

    public ICollection<Project>? Projects { get; set; }
}
