using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoftwareCompaniesManagement.Api.Data;
using SoftwareCompaniesManagement.Api.DTO.CreateDto;
using SoftwareCompaniesManagement.Api.DTO.GetDto;
using SoftwareCompaniesManagement.Api.DTO.UpdateDto;
using SoftwareCompaniesManagement.Model;
using SoftwareCompaniesManagement.Model.ValuesCheck;

namespace SoftwareCompaniesManagement.Api.MapEndpoints
{
    public static class ProjectTechnologiesEndpoints
    {
        public static RouteGroupBuilder MapProjectTechnologiesEndpoints(this WebApplication app)
        {
            var projectTechnologiesGroup = app.MapGroup("companies/{companyId}/projects/{projectId}/technologies").WithParameterValidation();

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProjectTechnology, ProjectTechnologyDto>();
                cfg.CreateMap<ProjectTechnologyDto, ProjectTechnology>();
                cfg.CreateMap<ProjectTechnology, CreateProjectTechnologyDto>();
                cfg.CreateMap<CreateProjectTechnologyDto, ProjectTechnology>();
                cfg.CreateMap<ProjectTechnology, UpdateProjectTechnologyDto>();
                cfg.CreateMap<UpdateProjectTechnologyDto, ProjectTechnology>();
            });

            var projectTechnologiesMapper = mapperConfiguration.CreateMapper();

            projectTechnologiesGroup.MapGet("", (CompaniesContext dbContext, HttpContext httpContext, int companyId, int projectId) =>
            {
                var token = TokenDecoder.DecodeToken(httpContext);

                if (token is null)
                {
                    return Results.Unauthorized();
                }

                var role = token.FindFirst("_role").Value;

                var project = dbContext.Projects.Find(projectId);

                if(project is null)
                {
                    return Results.NotFound();
                }

                var managerId = project.ManagerId;

                if (int.Parse(token.FindFirst("_infoId").Value) != managerId ||
                    role != "project_manager" &&
                    role != "company")
                {
                    bool unauthorized = true;

                    if (role == "developer")
                    {
                        var infoId = int.Parse(token.FindFirst("_infoId").Value);

                        var developerProject = dbContext.DeveloperProjects.Where(devProj => devProj.DeveloperId == infoId && devProj.ProjectId == projectId).Any();

                        if (!developerProject)
                        {
                            return Results.Unauthorized();
                        }
                        else
                        {
                            unauthorized = false;
                        }
                    }

                    if(unauthorized)
                    {
                        return Results.Unauthorized();
                    }
                }

                var technologies = dbContext.ProjectTechnologies.Where(projTech => projTech.ProjectId == projectId);

                return Results.Ok(technologies.Select(projectTechnologiesMapper.Map<ProjectTechnology, ProjectTechnologyDto>));
            });

            projectTechnologiesGroup.MapPost("", (CompaniesContext dbContext, HttpContext httpContext, int companyId, int projectId, CreateProjectTechnologyDto dto) =>
            {
                var token = TokenDecoder.DecodeToken(httpContext);

                if (token is null)
                {
                    return Results.Unauthorized();
                }

                var role = token.FindFirst("_role").Value;

                var project = dbContext.Projects.Find(projectId);

                if (project is null)
                {
                    return Results.NotFound();
                }

                var managerId = project.ManagerId;

                if (int.Parse(token.FindFirst("_infoId").Value) != managerId &&
                    int.Parse(token.FindFirst("_infoId").Value) != companyId ||
                    role != "project_manager" &&
                    role != "company")
                {
                    return Results.Unauthorized();
                }

                dbContext.ProjectTechnologies.Add(projectTechnologiesMapper.Map<CreateProjectTechnologyDto, ProjectTechnology>(dto));
                dbContext.SaveChanges();

                return Results.Created();
            });

            projectTechnologiesGroup.MapDelete("{projectTechnoogyId}", (CompaniesContext dbContext, HttpContext httpContext, int companyId, int projectId, int projectTechnoogyId) =>
            {
                var token = TokenDecoder.DecodeToken(httpContext);

                if (token is null)
                {
                    return Results.Unauthorized();
                }

                var role = token.FindFirst("_role").Value;

                var project = dbContext.Projects.Find(projectId);

                if (project is null)
                {
                    return Results.NotFound();
                }

                var managerId = project.ManagerId;

                if (int.Parse(token.FindFirst("_infoId").Value) != managerId &&
                    int.Parse(token.FindFirst("_infoId").Value) != companyId ||
                    role != "project_manager" &&
                    role != "company")
                {
                    return Results.Unauthorized();
                }

                dbContext.ProjectTechnologies.Where(pt => pt.TechnologyId == projectTechnoogyId && pt.ProjectId == projectId).ExecuteDelete();
                dbContext.SaveChanges();

                return Results.Created();
            });

            return projectTechnologiesGroup;
        }

    }
}
