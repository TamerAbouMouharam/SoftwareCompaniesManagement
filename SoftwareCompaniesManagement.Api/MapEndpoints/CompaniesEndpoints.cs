using AutoMapper;
using SoftwareCompaniesManagement.Api.Data;
using SoftwareCompaniesManagement.Api.DTO.CreateDto;
using SoftwareCompaniesManagement.Api.DTO.GetDto;
using SoftwareCompaniesManagement.Model;

namespace SoftwareCompaniesManagement.Api.MapEndpoints;

public static class CompaniesEndpoints
{
    public static RouteGroupBuilder MapCompaniesEndpoints(this WebApplication app)
    {
        var mapping_configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Company, CompanyDto>();
            cfg.CreateMap<Company, CreateCompanyDto>();
            cfg.CreateMap<CompanyDto, Company>();
            cfg.CreateMap<CreateCompanyDto, Company>();
        });

        var companies_mapper = mapping_configuration.CreateMapper();

        var companiesGroup = app.MapGroup("companies").WithParameterValidation();

        companiesGroup.MapGet("{id}", (CompaniesContext dbContext, int id) =>
        {
            var company = dbContext.Companies.Find(id);

            if(company is null) 
            {
                return Results.NotFound("This company does not exist.");
            }
            else
            {
                return Results.Ok(companies_mapper.Map<CompanyDto>(company));
            }
        }).WithName("companyGET");

        companiesGroup.MapPost("", (CompaniesContext dbContext, CreateCompanyDto companyDto) =>
        {
            var company = companies_mapper.Map<Company>(companyDto);

            dbContext.Companies.Add(company);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute("companyGET", new { id = company.Id }, companies_mapper.Map<CompanyDto>(company));
        });

        return companiesGroup;
    }
}