using SoftwareCompaniesManagement.Api.Data;

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

            suggestionGroup.MapGet("", (CompaniesContext dbContext, HttpContext httpContext, int companyId, int projectId) =>
            {
                var technologies = dbContext.ProjectTechnologies.Where(pt => pt.ProjectId == projectId).Select(pt => pt.TechnologyId);
                var developerTechnologies = dbContext.DeveloperTechnologies.Where(dt => technologies.Contains(dt.TechnologyId));

            });

            return suggestionGroup;
        }
    }
}
