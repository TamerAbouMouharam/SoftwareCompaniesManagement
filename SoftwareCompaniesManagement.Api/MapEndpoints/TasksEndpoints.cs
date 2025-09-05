using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using SoftwareCompaniesManagement.Api.Data;
using SoftwareCompaniesManagement.Api.DTO.CreateDto;
using SoftwareCompaniesManagement.Api.DTO.GetDto;
using SoftwareCompaniesManagement.Api.DTO.UpdateDto;

namespace SoftwareCompaniesManagement.Api.MapEndpoints
{
    public static class TasksEndpoints
    {
        public static RouteGroupBuilder MapTasksEndpoints(this WebApplication app)
        {
            var tasksGroup = app.MapGroup("companies/{companyId}/projects/{projectId}/tasks");

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Model.Task, TaskDto>();
                cfg.CreateMap<TaskDto, Model.Task>();
                cfg.CreateMap<Model.Task, CreateTaskDto>();
                cfg.CreateMap<CreateTaskDto, Model.Task>();
                cfg.CreateMap<Model.Task, UpdateTaskDto>();
                cfg.CreateMap<UpdateTaskDto, Model.Task>();
            });

            var tasksMapper = mapperConfiguration.CreateMapper();

            tasksGroup.MapGet("", (CompaniesContext dbContext, HttpContext httpContext, int companyId, int projectId) =>
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
                            unauthorized = false;
                        }
                    }

                    if (unauthorized)
                    {
                        return Results.Unauthorized();
                    }
                }

                var tasks = dbContext.Tasks.Where(task => task.ProjectId == projectId);

                if(role == "developer")
                {
                    tasks = tasks.Where(task => task.Status != "canceled");
                }

                return Results.Ok(tasks.Select(task => tasksMapper.Map<Model.Task, TaskDto>(task)));
            });

            tasksGroup.MapPost("", (CompaniesContext dbContext, HttpContext httpContext, int companyId, int projectId, CreateTaskDto taskDto) =>
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

                if (int.Parse(token.FindFirst("_infoId").Value) != managerId ||
                    role != "project_manager" &&
                    role != "company")
                {
                    return Results.Unauthorized();
                }

                var task = tasksMapper.Map<CreateTaskDto, Model.Task>(taskDto);

                dbContext.Tasks.Add(task);
                dbContext.SaveChanges();

                return Results.Created();
            });

            tasksGroup.MapDelete("{taskId}", (CompaniesContext dbContext, HttpContext httpContext, int companyId, int projectId, int taskId) =>
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

                if (int.Parse(token.FindFirst("_infoId").Value) != managerId ||
                    role != "project_manager" &&
                    role != "company")
                {
                    return Results.Unauthorized();
                }

                dbContext.Tasks.Where(task => task.Id == taskId).ExecuteDelete();
                dbContext.SaveChanges();

                return Results.NoContent();
            });

            tasksGroup.MapPut("{taskId}/lock", (CompaniesContext dbContext, HttpContext httpContext, int companyId, int projectId, int taskId) =>
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

                bool unauthorized = true;

                if (role == "developer")
                {
                    var infoId = int.Parse(token.FindFirst("_infoId").Value);

                    var developerProject = dbContext.DeveloperProjects.Where(devProj => devProj.DeveloperId == infoId && devProj.ProjectId == projectId).Any();

                    if (!developerProject)
                    {
                        unauthorized = false;
                    }
                }

                if (unauthorized)
                {
                    return Results.Unauthorized();
                }

                var task = dbContext.Tasks.Find(taskId);

                if(task.Status == "created")
                {
                    task.Status = "started";
                    task.DeveloperId = int.Parse(token.FindFirst("_infoId").Value);
                    dbContext.SaveChanges();
                }
                else
                {
                    return Results.Unauthorized();
                }

                return Results.NoContent();
            });

            tasksGroup.MapPut("{taskId}/done", (CompaniesContext dbContext, HttpContext httpContext, int companyId, int projectId, int taskId) =>
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

                bool unauthorized = true;

                if (role == "developer")
                {
                    var infoId = int.Parse(token.FindFirst("_infoId").Value);

                    var developerProject = dbContext.DeveloperProjects.Where(devProj => devProj.DeveloperId == infoId && devProj.ProjectId == projectId).Any();

                    if (!developerProject)
                    {
                        unauthorized = false;
                    }
                }

                if (unauthorized)
                {
                    return Results.Unauthorized();
                }

                
                var task = dbContext.Tasks.Find(taskId);

                if (task.Status == "started")
                {
                    task.Status = "done";
                    var expectedTime = task.EndDate.ToDateTime(TimeOnly.FromDateTime(DateTime.Now)).Ticks - task.StartDate.ToDateTime(TimeOnly.FromDateTime(DateTime.Now)).Ticks;
                    task.ActualEffort = expectedTime / (DateTime.Now.Ticks - task.StartDate.ToDateTime(TimeOnly.FromDateTime(DateTime.Now)).Ticks);
                    dbContext.SaveChanges();
                }
                else
                {
                    return Results.Conflict();
                }

                return Results.NoContent();
            });

            tasksGroup.MapPut("{taskId}/cancel", (CompaniesContext dbContext, HttpContext httpContext, int companyId, int projectId, int taskId) =>
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

                if (int.Parse(token.FindFirst("_infoId").Value) != managerId ||
                    role != "project_manager" &&
                    role != "company")
                {
                    return Results.Unauthorized();
                }

                var task = dbContext.Tasks.Find(taskId);
                task.Status = "canceled";
                dbContext.SaveChanges();

                return Results.NoContent();
            });

            tasksGroup.MapPut("{taskId}/accept", (CompaniesContext dbContext, HttpContext httpContext, int companyId, int projectId, int taskId) =>
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

                if (int.Parse(token.FindFirst("_infoId").Value) != managerId ||
                    role != "project_manager" &&
                    role != "company")
                {
                    return Results.Unauthorized();
                }

                var task = dbContext.Tasks.Find(taskId);
                if(task.Status == "done")
                {
                    var numTask = dbContext.Tasks.Where(task => task.ProjectId == projectId).Count();
                    var projectPoints = dbContext.Projects.Where(project => project.Id == projectId).Select(project => project.ProjectPoints).First();
                    double? addedPoints = 0.5 * task.Priority + 0.3 * task.Complexity + 0.2 * task.ActualEffort + projectPoints / numTask;
                    var developer = dbContext.Developers.Find(task.DeveloperId);
                    developer.Points += (double)addedPoints;
                }
                else
                {
                    return Results.Conflict();
                }
                dbContext.SaveChanges();

                return Results.NoContent();
            });

            return tasksGroup;
        }
    }
}
