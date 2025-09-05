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
            var suggestionGroup = app.MapGroup("companies/{companyId}/projects/{projectId}/suggest");

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

                var technologies = dbContext.ProjectTechnologies.Where(pt => pt.ProjectId == projectId).Select(pt => pt.TechnologyId);
                var developers = dbContext.DeveloperTechnologies.Where(dt => technologies.Contains(dt.TechnologyId))
                    .GroupBy(dt => dt.DeveloperId)
                    .Select(g => new { Id = g.Key, AvgPoints = g.Average(dt => 0.1 * dt.ExperienceYears + 0.2 * experienceMapping(dt.ExperienceLevel)) + 0.7 * dbContext.Developers.Find(g.Key).Points })
                    .OrderByDescending(g => g.AvgPoints)
                    .Select(g => g.Id)
                    .Select(id => dbContext.Developers.Find(id));

                return Results.Ok(developers.Select(developerMapper.Map<Developer, DeveloperDto>));
            });

            return suggestionGroup;
        }
    }
}
