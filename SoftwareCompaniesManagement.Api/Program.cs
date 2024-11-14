using Microsoft.EntityFrameworkCore;
using SoftwareCompaniesManagement.Api.Data;
using SoftwareCompaniesManagement.Api.MapEndpoints;

var builder = WebApplication.CreateBuilder(args);

string connection_string = builder.Configuration.GetConnectionString("db_source")!;

builder.Services.AddSqlite<CompaniesContext>(connection_string);

var app = builder.Build();

app.MapAccountsEndpoints();

app.Run();
