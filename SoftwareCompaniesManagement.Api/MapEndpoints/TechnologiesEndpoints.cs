using SoftwareCompaniesManagement.Api.Data;

namespace SoftwareCompaniesManagement.Api.MapEndpoints
{
    public static class TechnologiesEndpoints
    {
        public static void MapTechnologiesEndpoints(this WebApplication app)
        {
            app.MapGet("/technologies/{technologyId}", (CompaniesContext dbContext, int technologyId) =>
            {
                var technology = dbContext.Technologies.Find(technologyId);

                return Results.Ok(technology);
            });

            app.MapGet("/technologies", (CompaniesContext dbContext) =>
            { 

                return Results.Ok(dbContext.Technologies);
            });
        }
    }
}
