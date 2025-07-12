using SoftwareCompaniesManagement.Model;

namespace SoftwareCompaniesManagement.Api.Data
{
    public class SeedTechnologies
    {
        public static List<Technology> Technologies = new()
        {
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
            },
            new Technology()
            {
                Id = 5,
                TechnologyName = "Angular",
                Description = "Frontend Development Library"
            },
            new Technology()
            {
                Id = 6,
                TechnologyName = "WinForms",
                Description = ".NET Technology For Building Windows Desktop Apps"
            }
        };
    }
}
