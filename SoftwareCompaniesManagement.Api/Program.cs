using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoftwareCompaniesManagement.Api.Data;
using SoftwareCompaniesManagement.Api.DTO.GetDto;
using SoftwareCompaniesManagement.Api.MapEndpoints;
using SoftwareCompaniesManagement.Model;

var builder = WebApplication.CreateBuilder(args);

string connection_string = builder.Configuration.GetConnectionString("db_source")!;

builder.Services.AddSqlite<CompaniesContext>(connection_string);

builder.Services.AddDataProtection();
builder.Services.AddCors(policies => policies.AddPolicy("AnyCORS", policy => policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod().AllowCredentials()));

var app = builder.Build();

app.MapAccountsEndpoints();
app.MapCompaniesEndpoints();

app.UseCors("AnyCORS");

var scope = app.Services.CreateScope();

var dbContext = scope.ServiceProvider.GetRequiredService<CompaniesContext>();

dbContext.Database.Migrate();

app.Run();
