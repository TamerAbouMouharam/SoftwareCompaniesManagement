using SoftwareCompaniesManagement.Api.MapEndpoints.CookieManagement;
using Microsoft.AspNetCore.DataProtection;
using SoftwareCompaniesManagement.Api.Data;
using SoftwareCompaniesManagement.Model;
using AutoMapper;
using SoftwareCompaniesManagement.Api.DTO.GetDto;
using SoftwareCompaniesManagement.Api.DTO.CreateDto;

namespace SoftwareCompaniesManagement.Api.MapEndpoints;

public static class EmployeesEndpoints
{
    public static RouteGroupBuilder MapEmployeesEndpoints(this WebApplication app)
    {
        var employeesGroup = app.MapGroup("companies/{companyId}/employees");

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

        employeesGroup.MapGet("{id}", (CompaniesContext dbContext, int companyId, int id) =>
        {
            var employee = dbContext.Employees.Find(id);

            var employeeDto = employeeMapper.Map<EmployeeDto>(employee);

            return Results.Ok(employeeDto);
        });

        return employeesGroup;
    }
}
