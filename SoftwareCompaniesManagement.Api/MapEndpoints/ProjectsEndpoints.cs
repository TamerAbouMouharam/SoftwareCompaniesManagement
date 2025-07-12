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
            /*var projectsGroup = app.MapGroup("companies/{companyId}/projects");

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

                if (role != "company" && role != "project_manager" || int.Parse(sentCompanyId) != companyId)
                {
                    return Results.Unauthorized();
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

                if (role != "company" && role != "project_manager" || int.Parse(sentCompanyId) != companyId)
                {
                    return Results.Unauthorized();
                }

                var project = projectMapper.Map<CreateProjectDto, Project>(projectDto);

                dbContext.Projects.Add(project);
                dbContext.SaveChanges();

                return Results.CreatedAtRoute("GetProject", new { projectId = project.Id, companyId }, projectMapper.Map<Project, ProjectDto>(project));
            });

            projectsGroup.MapDelete("{projectId}", (CompaniesContext dbContext, HttpContext httpContext, CreateProjectDto projectDto, int companyId, int projectId) =>
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

                var project = projectMapper.Map<CreateProjectDto, Project>(projectDto);

                dbContext.Projects.Where(project => project.Id == projectId).ExecuteDelete();
                dbContext.SaveChanges();

                return Results.CreatedAtRoute("GetProject", new { projectId = project.Id, companyId }, projectMapper.Map<Project, ProjectDto>(project));
            });


            return projectsGroup;*/

            return null;

        } 
    }
}
