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
        var employeesGroup = app.MapGroup("employees");

        var mapperConfiguration = new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<Employee, EmployeeDto>();
            cfg.CreateMap<EmployeeDto, Employee>();
            cfg.CreateMap<Employee, CreateEmployeeDto>();
            cfg.CreateMap<CreateEmployeeDto, Employee>();
        });

        var employeeMapper = mapperConfiguration.CreateMapper();

        employeesGroup.MapGet("all", (HttpContext httpContext, CompaniesContext dbContext, IDataProtectionProvider protectionProvider) => 
        {
            if(UserCheck.RoleCheck(httpContext, protectionProvider, "employeemanager"))
            {
                var username = CookieFields.GetField(httpContext, protectionProvider, "username");

                var accountId = dbContext.Accounts.Where(account => account.Username == username).First().Id;

                var companyId = dbContext.Employees.Where(employee => employee.AccountId == accountId).First().CompanyId;

                return Results.Ok(dbContext.Employees.Where(employee => employee.CompanyId == companyId).Select(employee => employeeMapper.Map<Employee, EmployeeDto>(employee)).ToList());
            }
            else
            {
                return Results.StatusCode(401);
            }
        });

        employeesGroup.MapGet("{id}", (HttpContext httpContext, CompaniesContext dbContext, IDataProtectionProvider protectionProvider, int id) =>
        {
            if(UserCheck.RoleCheck(httpContext, protectionProvider, "employeemanager"))
            {
                var username = CookieFields.GetField(httpContext, protectionProvider, "username");

                var accountId = dbContext.Accounts.Where(account => account.Username == username).First().Id;

                var companyId = dbContext.Employees.Where(employee => employee.AccountId == accountId).First().CompanyId;

                var employee = dbContext.Employees.Where(employee => employee.Id == id && employee.CompanyId == companyId).First();

                return Results.Ok(employeeMapper.Map<Employee, EmployeeDto>(employee));
            }
            else
            {
                return Results.StatusCode(401);
            }
        }).WithName("GetEmployee");

        employeesGroup.MapPost("", (HttpContext httpContext, CompaniesContext dbContext, IDataProtectionProvider protectionProvider, CreateEmployeeDto employeeDto) =>
        {
            var employee = employeeMapper.Map<CreateEmployeeDto, Employee>(employeeDto);

            if(UserCheck.RoleCheck(httpContext, protectionProvider, "employeemanager") || UserCheck.RoleCheck(httpContext, protectionProvider, "employee"))
            {
                dbContext.Employees.Add(employee);
                dbContext.SaveChanges();

                return Results.CreatedAtRoute("GetEmployee", new { id = employee.Id }, employeeMapper.Map<Employee, EmployeeDto>(employee));
            }
            else
            {
                return Results.StatusCode(401);
            }
        });

        return employeesGroup;
    }
}
