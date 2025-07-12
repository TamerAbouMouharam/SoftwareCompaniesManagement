using SoftwareCompaniesManagement.Api.MapEndpoints.CookieManagement;
using Microsoft.AspNetCore.DataProtection;
using SoftwareCompaniesManagement.Api.Data;
using SoftwareCompaniesManagement.Model;
using AutoMapper;
using SoftwareCompaniesManagement.Api.DTO.GetDto;
using SoftwareCompaniesManagement.Api.DTO.CreateDto;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace SoftwareCompaniesManagement.Api.MapEndpoints;

public static class EmployeesEndpoints
{
    public static RouteGroupBuilder MapEmployeesEndpoints(this WebApplication app)
    {
        var employeesGroup = app.MapGroup("companies/{companyId}/employees").WithParameterValidation();

        var mapperConfiguration = new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<Employee, EmployeeDto>();
            cfg.CreateMap<EmployeeDto, Employee>();
            cfg.CreateMap<Employee, CreateEmployeeDto>();
            cfg.CreateMap<CreateEmployeeDto, Employee>();
        });

        var employeeMapper = mapperConfiguration.CreateMapper();

        employeesGroup.MapGet("", (CompaniesContext dbContext, HttpContext httpContext, int companyId) =>
        {
            var token = TokenDecoder.DecodeToken(httpContext);

            if (token is null)
            {
                return Results.Unauthorized();
            }

            var role = token.FindFirst("_role").Value;
            var sentCompanyId = token.FindFirst("_companyId").Value;

            if (role != "company" && role != "employee_manager" || int.Parse(sentCompanyId) != companyId)
            {
                return Results.Unauthorized();
            }

            var employees = dbContext.Employees.Where(employee => employee.CompanyId == companyId).ToList();

            var employeeDTOs = employees.Select(employee => employeeMapper.Map<EmployeeDto>(employee)).ToList();

            return Results.Ok(employeeDTOs);
        });

        employeesGroup.MapGet("{employeeId}", (CompaniesContext dbContext, int companyId, int employeeId) =>
        {
            var employee = dbContext.Employees.Find(employeeId);

            var employeeDto = employeeMapper.Map<EmployeeDto>(employee);

            if(employee is not null)
            {
                return Results.Ok(employeeDto);
            }
            else
            {
                return Results.NotFound("There is no such employee");
            }
        }).WithName("GetEmployee");

        employeesGroup.MapPost("", (CompaniesContext dbContext, int companyId, CreateEmployeeDto employeeDto) => 
        {
            var employee = employeeMapper.Map<Employee>(employeeDto);

            dbContext.Employees.Add(employee);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute("GetEmployee", new { employeeId = employee.Id }, employeeMapper.Map<EmployeeDto>(employee));
        });

        employeesGroup.MapPut("{employeeId}", (CompaniesContext dbContext, HttpContext httpContext, int companyId, CreateEmployeeDto employeeDto, int employeeId) =>
        {
            var token = TokenDecoder.DecodeToken(httpContext);

            if (token is null)
            {
                return Results.Unauthorized();
            }

            var role = token.FindFirst("_role").Value;
            var sentCompanyId = token.FindFirst("_companyId").Value;

            if(role != "company" && role != "employee_manager" || int.Parse(sentCompanyId) != companyId)
            {
                return Results.Unauthorized();
            }

            var employee = dbContext.Employees.Find(employeeId);
            dbContext.Employees.Entry(employee).CurrentValues.SetValues(employeeDto);
            dbContext.SaveChanges();

            return Results.NoContent();
        });

        return employeesGroup;
    }
}
