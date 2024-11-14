using Microsoft.EntityFrameworkCore;
using SoftwareCompaniesManagement.Model;

namespace SoftwareCompaniesManagement.Api.Data;

public class CompaniesContext(DbContextOptions<CompaniesContext> options) : DbContext(options)
{
    public DbSet<Company> Companies { get; set; }

    public DbSet<Account> Accounts { get; set; }

    public DbSet<Developer> Developers { get; set; }

    public DbSet<DeveloperProject> DeveloperProjects { get; set; }

    public DbSet<DeveloperTechnology> DeveloperTechnologies { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Project> Projects { get; set; }

    public DbSet<ProjectTechnology> ProjectTechnologies { get; set; }

    public DbSet<SoftwareCompaniesManagement.Model.Task> Tasks { get; set; }
    
    public DbSet<Technology> Technologies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite();
    }
}
