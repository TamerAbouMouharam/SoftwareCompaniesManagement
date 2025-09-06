using AutoMapper;
using SoftwareCompaniesManagement.Model;
using SoftwareCompaniesManagement.Api.DTO.GetDto;
using SoftwareCompaniesManagement.Api.DTO.CreateDto;
using SoftwareCompaniesManagement.Api.DTO.UpdateDto;
using SoftwareCompaniesManagement.Api.Data;

namespace SoftwareCompaniesManagement.Api.MapEndpoints
{
    public static class DeveloperProjectsEndpoints
    {
        public static RouteGroupBuilder MapDeveloperProjectsEndpoints(this WebApplication app)
        {
            var projectDevelopersGroup = app.MapGroup("companies/{companyId}/projects/{projectId}/developers").WithParameterValidation();

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DeveloperProject, DeveloperProjectDto>();
                cfg.CreateMap<DeveloperProjectDto, DeveloperProject>();
                cfg.CreateMap<DeveloperProject, CreateDeveloperProjectDto>();
                cfg.CreateMap<CreateDeveloperProjectDto, DeveloperProject>();
                cfg.CreateMap<DeveloperProject, UpdateDeveloperProjectDto>();
                cfg.CreateMap<UpdateDeveloperProjectDto, DeveloperProject>();
            });

            var projectDevelopersMapper = mapperConfiguration.CreateMapper();

            projectDevelopersGroup.MapGet("", (CompaniesContext dbContext, HttpContext httpContext, int companyId, int projectId) =>
            {
                var token = TokenDecoder.DecodeToken(httpContext);

                if (token is null)
                {
                    return Results.Unauthorized();
                }

                var role = token.FindFirst("_role").Value;
                var sentCompanyId = token.FindFirst("_companyId").Value;

                if (role != "company" && role != "project_manager" && role != "developer" || int.Parse(sentCompanyId) != companyId)
                {
                    return Results.Unauthorized();
                }

                if (role == "developer")
                {
                    var infoId = int.Parse(token.FindFirst("_infoId").Value);

                    var developerProject = dbContext.DeveloperProjects.Where(devProj => devProj.DeveloperId == infoId && devProj.ProjectId == projectId).Any();

                    if (!developerProject)
                    {
                        return Results.Unauthorized();
                    }
                }

                var developers = dbContext.DeveloperProjects.Where(dp => dp.ProjectId == projectId);

                if (developers is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(developers);
            });

            projectDevelopersGroup.MapPost("", (CompaniesContext dbContext, HttpContext httpContext, int companyId, int projectId, CreateDeveloperProjectDto dto) =>
            {
                var token = TokenDecoder.DecodeToken(httpContext);

                if (token is null)
                {
                    return Results.Unauthorized();
                }

                var role = token.FindFirst("_role").Value;
                var sentCompanyId = token.FindFirst("_companyId").Value;

                if (role != "company" && role != "project_manager" || int.Parse(sentCompanyId) != companyId)
                {
                    return Results.Unauthorized();
                }

                dbContext.DeveloperProjects.Add(projectDevelopersMapper.Map<CreateDeveloperProjectDto, DeveloperProject>(dto));
                dbContext.SaveChanges();

                return Results.Created();
            });

            return projectDevelopersGroup;
        }
    }
}
