using System.ComponentModel.DataAnnotations;

namespace SoftwareCompaniesManagement.Model;

public class Account
{
    [Key]
    public int Id { get; set; }

    public required string Username { get; set; }

    public required string Password { get; set; }
}
