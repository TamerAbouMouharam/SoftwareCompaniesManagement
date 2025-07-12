using System.ComponentModel.DataAnnotations;

namespace SoftwareCompaniesManagement.Authorization.Model;

public class Account
{
    [Key]
    public int Id { get; set; }

    public int InfoId { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string Role { get; set; }

    public int CompanyId { get; set; }

    public bool IsActive { get; set; }
}
