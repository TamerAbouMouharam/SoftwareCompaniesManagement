using Microsoft.AspNetCore.DataProtection;
using SoftwareCompaniesManagement.Api.Data;
using SoftwareCompaniesManagement.Model;

namespace SoftwareCompaniesManagement.Api.MapEndpoints.CookieManagement;

static class UserCheck
{
    public static bool RoleCheck(HttpContext context, IDataProtectionProvider protectionProvider, string role)
    {
        var authCookie = context.Request.Headers["cookie"].FirstOrDefault(cookie => cookie!.StartsWith("auth"));

        if(authCookie is null)
        {
            return false;
        }
        else
        {
            var protector = protectionProvider.CreateProtector("authentication");

            authCookie = protector.Unprotect(authCookie);

            var cookieRole = authCookie.Split("=").Last().Split(";").First(field => field.StartsWith("role")).Split(":").Last();

            if(cookieRole == role)
            {
                return true;
            }
            else
            {
                return false;
            }
        }   
    }

    public static bool LoginCheck(HttpContext context, IDataProtectionProvider protectionProvider, string username, string password)
    {
        var authCookie = context.Request.Headers["cookie"].FirstOrDefault(cookie => cookie!.StartsWith("auth"));

        if(authCookie is null)
        {
            return false;
        }
        else
        {
            var protector = protectionProvider.CreateProtector("authentication");

            authCookie = protector.Unprotect(authCookie);

            var cookieUsername = authCookie.Split("=").Last().Split(";").First(field => field.StartsWith("username")).Split(":").Last();

            var cookiePassword = authCookie.Split("=").Last().Split(";").First(field => field.StartsWith("password")).Split(":").Last();

            if(cookieUsername == username && cookiePassword == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        } 
    }

    public static bool LoggedIn(HttpContext context, IDataProtectionProvider protectionProvider)
    {
        var authCookie = context.Request.Headers["cookie"].FirstOrDefault(cookie => cookie!.StartsWith("auth"));

        if(authCookie is null)
        {
            return false;
        }
        else
        {
            var loginState = authCookie.Split("=").Last().Split(";").First(field => field.StartsWith("login")).Split(":").Last();

            if(loginState == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public static string? GetCompany(HttpContext context, CompaniesContext dbContext, IDataProtectionProvider protectionProvider)
    {
        var authCookie = context.Request.Headers["cookie"].FirstOrDefault(cookie => cookie!.StartsWith("auth"));

        if(authCookie is null)
        {
            return string.Empty;
        }
        else
        {
            var username = authCookie.Split("=").Last().Split(";").First(field => field.StartsWith("username")).Split(":").Last();

            var accountId = dbContext.Accounts.Where(account_ => account_.Username == username).First().Id;

            if(dbContext.Employees.Where(employee_ => employee_.AccountId == accountId) is ICollection<Employee> employee && employee.Count() > 0)
            {
                var companyId = employee.First().CompanyId;

                return dbContext.Companies.Find(companyId)!.CompanyName;
            }
            else
            {
                if(dbContext.Companies.Where(company_ => company_.AccountId == accountId).FirstOrDefault() is Company company && !(company is null))
                {
                    return company.CompanyName;
                }

                if(dbContext.Developers.Where(developer_ => developer_.AccountId == accountId).FirstOrDefault() is Developer developer && !(developer is null))
                {
                    var companyId = developer.CompanyId;

                    return dbContext.Companies.Find(companyId)!.CompanyName;
                }

                return string.Empty;
            }
        }
    }
}
