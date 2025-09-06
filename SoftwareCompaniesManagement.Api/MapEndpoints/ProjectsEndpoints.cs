using System.ComponentModel.Design;
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
    public static class ProjectsEndpoints
    {
        public static RouteGroupBuilder MapProjectsEndpoints(this WebApplication app)
        {
            var projectsGroup = app.MapGroup("companies/{companyId}/projects").WithParameterValidation();

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Project, ProjectDto>();
                cfg.CreateMap<ProjectDto, Project>();
                cfg.CreateMap<Project, CreateProjectDto>();
                cfg.CreateMap<CreateProjectDto, Project>();
                cfg.CreateMap<Project, UpdateProjectDto>();
                cfg.CreateMap<UpdateProjectDto, Project>();
            });

            var projectMapper = mapperConfiguration.CreateMapper();

            projectsGroup.MapGet("", (CompaniesContext dbContext, HttpContext httpContext, int companyId) =>
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

                var projects = dbContext.Projects.Where(project => project.CompanyId == companyId);

                if(role == "developer")
                {
                    var infoId = int.Parse(token.FindFirst("_infoId").Value);

                    var developerProjects = dbContext.DeveloperProjects.Where(devProj => devProj.DeveloperId == infoId).Select(devProj => devProj.ProjectId).ToList();

                    projects = projects.Where(project => developerProjects.Contains(project.Id));
                }

                return Results.Ok(projects.ToList().Select(projectMapper.Map<Project, ProjectDto>).ToList());
            });

            projectsGroup.MapGet("{projectId}", (CompaniesContext dbContext, HttpContext httpContext, int companyId, int projectId) =>
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

                if(role == "developer")
                {
                    var infoId = int.Parse(token.FindFirst("_infoId").Value);

                    var developerProject = dbContext.DeveloperProjects.Where(devProj => devProj.DeveloperId == infoId && devProj.ProjectId == projectId).Any();

                    if(!developerProject)
                    {
                        return Results.Unauthorized();
                    }
                }

                var project = dbContext.Projects.FirstOrDefault(project => project.CompanyId == companyId && project.Id == projectId);

                if (project is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(projectMapper.Map<Project, ProjectDto>(project));
            }).WithName("GetProject");

            projectsGroup.MapPost("", (CompaniesContext dbContext, HttpContext httpContext, CreateProjectDto projectDto, int companyId) =>
            {
                var token = TokenDecoder.DecodeToken(httpContext);

                if (token is null)
                {
                    return Results.Unauthorized();
                }

                var role = token.FindFirst("_role").Value;
                var sentCompanyId = token.FindFirst("_companyId").Value;

                if (role != "project_manager" || int.Parse(sentCompanyId) != companyId)
                {
                    return Results.Unauthorized();
                }

                var infoId = int.Parse(token.FindFirst("_infoId").Value);

                var project = projectMapper.Map<CreateProjectDto, Project>(projectDto);
                project.ManagerId = infoId;

                dbContext.Projects.Add(project);
                dbContext.SaveChanges();

                return Results.CreatedAtRoute("GetProject", new { projectId = project.Id, companyId }, projectMapper.Map<Project, ProjectDto>(project));
            });

            projectsGroup.MapPut("{projectId}", (CompaniesContext dbContext, HttpContext httpContext, UpdateProjectDto projectDto, int companyId, int projectId) =>
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

                var infoId = int.Parse(token.FindFirst("_infoId").Value);

                var projectNewData = projectMapper.Map<UpdateProjectDto, Project>(projectDto);

                var project = dbContext.Projects.Find(projectId);

                if (infoId != project.ManagerId)
                {
                    return Results.Unauthorized();
                }

                dbContext.Projects.Entry(project).CurrentValues.SetValues(projectDto);
                dbContext.SaveChanges();

                return Results.NoContent();
            });

            projectsGroup.MapDelete("{projectId}", (CompaniesContext dbContext, HttpContext httpContext, int companyId, int projectId) =>
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

                var project = dbContext.Projects.Where(project => project.Id == projectId);

                if (role == "project_manager" && int.Parse(token.FindFirst("_infoId").Value) != project.FirstOrDefault().ManagerId)
                {
                    return Results.Unauthorized();
                }

                dbContext.Projects.Where(project => project.Id == projectId).ExecuteDelete();
                dbContext.SaveChanges();

                return Results.NoContent();
            });

            return projectsGroup;

        } 
    }
}
