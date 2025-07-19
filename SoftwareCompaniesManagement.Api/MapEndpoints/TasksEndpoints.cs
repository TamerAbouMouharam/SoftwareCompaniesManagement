//using AutoMapper;
//using SoftwareCompaniesManagement.Api.Data;
//using SoftwareCompaniesManagement.Api.DTO.CreateDto;
//using SoftwareCompaniesManagement.Api.DTO.GetDto;
//using SoftwareCompaniesManagement.Api.DTO.UpdateDto;

//namespace SoftwareCompaniesManagement.Api.MapEndpoints
//{
//    public static class TasksEndpoints
//    {
//        public static RouteGroupBuilder MapTasksEndpoints(this WebApplication app)
//        {
//            var tasksGroup = app.MapGroup("companies/{companyId}/projects/{projectId}/tasks");

//            var mapperConfiguration = new MapperConfiguration(cfg =>
//            {
//                cfg.CreateMap<Model.Task, TaskDto>();
//                cfg.CreateMap<TaskDto, Model.Task>();
//                cfg.CreateMap<Model.Task, CreateTaskDto>();
//                cfg.CreateMap<CreateTaskDto, Model.Task>();
//                cfg.CreateMap<Model.Task, UpdateTaskDto>();
//                cfg.CreateMap<UpdateTaskDto, Model.Task>();
//            });

//            var tasksMapper = mapperConfiguration.CreateMapper();

//            app.MapGet("", (CompaniesContext dbContext, HttpContext httpContext, int companyId, int projectId) =>
//            {
//                var token = TokenDecoder.DecodeToken(httpContext);

//                if (token is null)
//                {
//                    return Results.Unauthorized();
//                }

//                var role = token.FindFirst("_role").Value;

//                var project = dbContext.Projects.Find(projectId);

//                if (project is null)
//                {
//                    return Results.NotFound();
//                }

//                var managerId = project.ManagerId;

//                if (int.Parse(token.FindFirst("_infoId").Value) != managerId ||
//                    role != "project_manager" &&
//                    role != "company")
//                {
//                    bool unauthorized = true;

//                    if (role == "developer")
//                    {
//                        var infoId = int.Parse(token.FindFirst("_infoId").Value);

//                        var developerProject = dbContext.DeveloperProjects.Where(devProj => devProj.DeveloperId == infoId && devProj.ProjectId == projectId).Any();

//                        if (!developerProject)
//                        {
//                            unauthorized = false;
//                        }
//                    }

//                    if (unauthorized)
//                    {
//                        return Results.Unauthorized();
//                    }
//                }


//            });

//            return tasksGroup;
//        }
//    }
//}
