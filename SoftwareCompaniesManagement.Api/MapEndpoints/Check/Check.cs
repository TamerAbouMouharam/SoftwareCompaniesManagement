using Microsoft.AspNetCore.DataProtection;

namespace SoftwareCompaniesManagement.Api.MapEndpoints.RoleCheck;

static class Check
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
}
