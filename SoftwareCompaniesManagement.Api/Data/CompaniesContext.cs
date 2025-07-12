using Microsoft.EntityFrameworkCore;
using SoftwareCompaniesManagement.Model;

namespace SoftwareCompaniesManagement.Api.Data;

public class CompaniesContext(DbContextOptions<CompaniesContext> options) : DbContext(options)
{
    public DbSet<Company> Companies { get; set; } = null!;

    public DbSet<Account> Accounts { get; set; } = null!;

    public DbSet<Developer> Developers { get; set; } = null!;

    public DbSet<DeveloperProject> DeveloperProjects { get; set; } = null!;

    public DbSet<DeveloperTechnology> DeveloperTechnologies { get; set; } = null!;

    public DbSet<Employee> Employees { get; set; } = null!;

    public DbSet<Project> Projects { get; set; } = null!;

    public DbSet<ProjectTechnology> ProjectTechnologies { get; set; } = null!;

    public DbSet<SoftwareCompaniesManagement.Model.Task> Tasks { get; set; } = null!;
    
    public DbSet<Technology> Technologies { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Technology>().HasData(
            new Technology()
            {
                Id = 1,
                TechnologyName = "Microsoft SQL Server",
                Description = "RDBMS"
            },
            new Technology()
            {
                Id = 2,
                TechnologyName = "React",
                Description = "Frontend Development Library"
            },
            new Technology()
            {
                Id = 3,
                TechnologyName = "ASP.NET Core",
                Description = "Backend Development Tools Uisng C#"
            },
            new Technology()
            {
                Id = 4,
                TechnologyName = ".NET MAUI",
                Description = "A Cross-Platform App Development Framework Using C#"
            }
        );
    }
}
