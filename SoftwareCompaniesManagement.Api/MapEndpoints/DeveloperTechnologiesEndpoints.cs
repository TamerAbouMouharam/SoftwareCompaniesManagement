using AutoMapper;
using SoftwareCompaniesManagement.Api.Data;
using SoftwareCompaniesManagement.Api.DTO.CreateDto;
using SoftwareCompaniesManagement.Api.DTO.GetDto;
using SoftwareCompaniesManagement.Api.DTO.UpdateDto;
using SoftwareCompaniesManagement.Model;

namespace SoftwareCompaniesManagement.Api.MapEndpoints
{
    public static class DeveloperTechnologiesEndpoints
    {
        public static RouteGroupBuilder MapDeveloperTechnologiesEndpoints(this WebApplication app)
        {
            var developerTechnologiesGroup = app.MapGroup("companies/{companyId}/developers/{developerId}/technologies").WithParameterValidation();

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DeveloperTechnology, DeveloperTechnologyDto>();
                cfg.CreateMap<DeveloperTechnologyDto, DeveloperTechnology>();
                cfg.CreateMap<DeveloperTechnology, CreateDeveloperTechnologyDto>();
                cfg.CreateMap<CreateDeveloperTechnologyDto, DeveloperTechnology>();
                cfg.CreateMap<DeveloperTechnology, UpdateDeveloperTechnologyDto>();
                cfg.CreateMap<UpdateDeveloperTechnologyDto, DeveloperTechnology>();
            });

            var developerTechnologiesMapper = mapperConfiguration.CreateMapper();

            developerTechnologiesGroup.MapGet("", (CompaniesContext dbContext, HttpContext httpContext, int companyId, int developerId) =>
            {
                var token = TokenDecoder.DecodeToken(httpContext);

                if (token is null)
                {
                    return Results.Unauthorized();
                }

                if (int.Parse(token.FindFirst("_companyId").Value) != companyId ||
                    token.FindFirst("_role").Value != "developer" &&
                    token.FindFirst("_role").Value != "project_manager" &&
                    token.FindFirst("_role").Value != "company")
                {
                    return Results.Unauthorized();
                }

                var technologies = dbContext.DeveloperTechnologies.Where(devTech => devTech.DeveloperId == developerId);

                return Results.Ok(technologies.Select(developerTechnologiesMapper.Map<DeveloperTechnology, DeveloperTechnologyDto>));
            });

            developerTechnologiesGroup.MapPost("", (CompaniesContext dbContext, HttpContext httpContext, int companyId, int developerId, CreateDeveloperTechnologyDto dto) =>
            {
                var token = TokenDecoder.DecodeToken(httpContext);

                if (token is null)
                {
                    return Results.Unauthorized();
                }

                if (int.Parse(token.FindFirst("_companyId").Value) != companyId ||
                    token.FindFirst("_role").Value != "developer" &&
                    token.FindFirst("_role").Value != "project_manager" &&
                    token.FindFirst("_role").Value != "company")
                {
                    return Results.Unauthorized();
                }

                dbContext.DeveloperTechnologies.Add(developerTechnologiesMapper.Map<CreateDeveloperTechnologyDto, DeveloperTechnology>(dto));
                dbContext.SaveChanges();

                return Results.Created();
            });

            developerTechnologiesGroup.MapPut("{technologyId}", (CompaniesContext dbContext, HttpContext httpContext, int companyId, int developerId, int technologyId, UpdateDeveloperTechnologyDto dto) =>
            {
                var token = TokenDecoder.DecodeToken(httpContext);

                if (token is null)
                {
                    return Results.Unauthorized();
                }

                if (int.Parse(token.FindFirst("_companyId").Value) != companyId ||
                    token.FindFirst("_role").Value != "project_manager" &&
                    token.FindFirst("_role").Value != "company")
                {
                    return Results.Unauthorized();
                }

                var technology = dbContext.DeveloperTechnologies.Where(tech => tech.DeveloperId == developerId && tech.TechnologyId == technologyId).FirstOrDefault();

                if(technology is null)
                {
                    return Results.NotFound();
                }

                dbContext.DeveloperTechnologies.Entry(technology).CurrentValues.SetValues(developerTechnologiesMapper.Map<UpdateDeveloperTechnologyDto, DeveloperTechnology>(dto));
                dbContext.SaveChanges();

                return Results.NoContent();
            });

            return developerTechnologiesGroup;
        }
    }
}
