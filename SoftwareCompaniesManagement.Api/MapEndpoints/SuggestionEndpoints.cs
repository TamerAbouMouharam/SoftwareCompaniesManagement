using AutoMapper;
using SoftwareCompaniesManagement.Api.Data;
using SoftwareCompaniesManagement.Api.DTO.GetDto;
using SoftwareCompaniesManagement.Model;

namespace SoftwareCompaniesManagement.Api.MapEndpoints
{
    public static class SuggestionEndpoints
    {
        private static int experienceMapping(string experience)
        {
            switch (experience)
            {
                case "freshman":
                    return 1;
                case "beginner":
                    return 2;
                case "Intermediate":
                    return 3;
                case "advanced":
                    return 4;
                default:
                    return -1;
            }
        }

        public static RouteGroupBuilder MapSuggestionEndpoints(this WebApplication app)
        {
            var suggestionGroup = app.MapGroup("companies/{companyId}/projects/{projectId}/suggest").WithParameterValidation();

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Developer, DeveloperDto>();
                cfg.CreateMap<DeveloperDto, Developer>();
            });

            var developerMapper = mapperConfiguration.CreateMapper();

            suggestionGroup.MapGet("", (CompaniesContext dbContext, HttpContext httpContext, int companyId, int projectId) =>
            {
                var token = TokenDecoder.DecodeToken(httpContext);

                if (token is null)
                {
                    return Results.Unauthorized();
                }

                var role = token.FindFirst("_role").Value;
                var sentCompanyId = token.FindFirst("_companyId").Value;
                var infoId = token.FindFirst("_infoId").Value;

                if (role != "company" && role != "project_manager" || int.Parse(sentCompanyId) != companyId)
                {
                    return Results.Unauthorized();
                }

                if(role == "project_manager" && dbContext.Projects.Where(project => project.Id == projectId).Select(project => project.ManagerId).First() != int.Parse(infoId))
                {
                    return Results.Unauthorized();
                }

                var technologyIds = dbContext.ProjectTechnologies.Where(pt => pt.ProjectId == projectId).Select(pt => pt.TechnologyId);
                var developerExpMeasure = dbContext.DeveloperTechnologies.Where(dt => technologyIds.Contains(dt.TechnologyId)).ToList()
                    .GroupBy(dt => dt.DeveloperId)
                    .Select(g => new {Id = g.Key, Exp = 0.3 * g.Average(dt => dt.ExperienceYears + experienceMapping(dt.ExperienceLevel))}).ToList();

                var devProject = dbContext.DeveloperProjects.Where(dp => dp.ProjectId == projectId)
                    .Select(dp => dp.DeveloperId)
                    .ToList();

                List<dynamic> developerIds = new();
                foreach (var dem in developerExpMeasure)
                {
                    var devId = new { dem.Id, Score = dem.Exp + 0.7 * dbContext.Developers.Find(dem.Id).Points };
                    Console.WriteLine(devId.Score);
                    var devTemp = dbContext.Developers.Find(dem.Id);

                    if (devTemp.CompanyId == companyId && !devProject.Contains(dem.Id))
                    {
                        developerIds.Add(devId);
                    }
                }

                return Results.Ok(developerIds.OrderByDescending(d => d.Score));
            });

            return suggestionGroup;
        }
    }
}
