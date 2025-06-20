using SoftwareCompaniesManagement.Api.MapEndpoints.CookieManagement;
using Microsoft.AspNetCore.DataProtection;
using SoftwareCompaniesManagement.Api.Data;
using SoftwareCompaniesManagement.Model;
using AutoMapper;
using SoftwareCompaniesManagement.Api.DTO.GetDto;
using SoftwareCompaniesManagement.Api.DTO.CreateDto;
using Microsoft.EntityFrameworkCore;

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

        employeesGroup.MapGet("", (CompaniesContext dbContext, int companyId) =>
        {
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

        return employeesGroup;
    }
}
