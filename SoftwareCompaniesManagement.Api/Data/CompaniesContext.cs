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

        modelBuilder.Entity<Technology>().HasData(SeedTechnologies.Technologies);
    }
}
