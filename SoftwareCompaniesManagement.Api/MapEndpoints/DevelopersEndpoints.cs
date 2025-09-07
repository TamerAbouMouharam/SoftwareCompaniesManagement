using AutoMapper;
using SoftwareCompaniesManagement.Api.DTO.GetDto;
using SoftwareCompaniesManagement.Api.DTO.CreateDto;
using SoftwareCompaniesManagement.Model;
using SoftwareCompaniesManagement.Api.DTO.UpdateDto;
using SoftwareCompaniesManagement.Api.Data;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace SoftwareCompaniesManagement.Api.MapEndpoints
{
    public static class DevelopersEndpoints
    {
        public static RouteGroupBuilder MapDevelopersEndpoints(this WebApplication app)
        {
            var developersGroup = app.MapGroup("companies/{companyId}/developers").WithParameterValidation();

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Developer, DeveloperDto>();
                cfg.CreateMap<DeveloperDto, Developer>();
                cfg.CreateMap<Developer, CreateDeveloperDto>();
                cfg.CreateMap<CreateDeveloperDto, Developer>();
                cfg.CreateMap<Developer, UpdateDeveloperDto>();
                cfg.CreateMap<UpdateDeveloperDto, Developer>();
            });

            var developerMapper = mapperConfiguration.CreateMapper();

            developersGroup.MapGet("", (CompaniesContext dbContext, HttpContext httpContext, int companyId) =>
            {
                var token = TokenDecoder.DecodeToken(httpContext);

                if(token is null)
                {
                    return Results.Unauthorized();
                }

                if (int.Parse(token.FindFirst("_companyId").Value) != companyId ||
                    token.FindFirst("_role").Value != "project_manager" &&
                    token.FindFirst("_role").Value != "employee_manager" && 
                    token.FindFirst("_role").Value != "company")
                {
                    return Results.Unauthorized();
                }

                var developers = dbContext.Developers.Where(developer => developer.CompanyId == companyId);

                return Results.Ok(developers.ToList().Select(developer => developerMapper.Map<Developer, DeveloperDto>(developer)));
            });

            developersGroup.MapGet("{developerId}", (CompaniesContext dbContext, HttpContext httpContext, int companyId, int developerId) =>
            {
                var token = TokenDecoder.DecodeToken(httpContext);

                if (token is null)
                {
                    return Results.Unauthorized();
                }

                if (int.Parse(token.FindFirst("_companyId").Value) != companyId ||
                    token.FindFirst("_role").Value != "project_manager" &&
                    token.FindFirst("_role").Value != "employee_manager" && 
                    token.FindFirst("_role").Value != "company" && 
                    token.FindFirst("_role").Value != "developer")
                {
                    return Results.Unauthorized();
                }

                var developer = dbContext.Developers.Find(developerId);

                return Results.Ok(developerMapper.Map<Developer, DeveloperDto>(developer));
            }).WithName("GetDeveloper");

            developersGroup.MapPost("", (CompaniesContext dbContext, HttpContext httpContext, int companyId, CreateDeveloperDto dto) =>
            {
                var develper = developerMapper.Map<CreateDeveloperDto, Developer>(dto);

                dbContext.Developers.Add(develper);
                dbContext.SaveChanges();

                return Results.CreatedAtRoute("GetDeveloper", new { companyId, developerId = develper.Id }, developerMapper.Map<Developer, DeveloperDto>(develper));
            });

            developersGroup.MapPut("{developerId}", (CompaniesContext dbContext, HttpContext httpContext, int companyId, int developerId, UpdateDeveloperDto dto) =>
            {
                var token = TokenDecoder.DecodeToken(httpContext);

                if (token is null)
                {
                    return Results.Unauthorized();
                }

                if (int.Parse(token.FindFirst("_companyId").Value) != companyId ||
                    token.FindFirst("_role").Value != "project_manager" &&
                    token.FindFirst("_role").Value != "employee_manager" && 
                    token.FindFirst("_role").Value != "company" && 
                    token.FindFirst("_role").Value != "developer")
                {
                    return Results.Unauthorized();
                }

                var developer = dbContext.Developers.Find(developerId);

                if(developer is null)
                {
                    return Results.NotFound();
                }

                dbContext.Developers.Entry(developer).CurrentValues.SetValues(dto);
                dbContext.SaveChanges();

                return Results.NoContent();
            });

            return developersGroup;
        }
    }
}
