using Microsoft.AspNetCore.DataProtection;

namespace SoftwareCompaniesManagement.Api.MapEndpoints.CookieManagement;

public static class CookieFields
{
    public static string GetField(HttpContext context, IDataProtectionProvider protectionProvider, string field)
    {
        var authCookie = context.Request.Headers["cookie"].FirstOrDefault(cookie => cookie!.StartsWith("auth"));

        var protector = protectionProvider.CreateProtector("authentication");

        authCookie = protector.Unprotect(authCookie);

        var cookieField = authCookie.Split("=").Last().Split(";").First(field_ => field_.StartsWith(field)).Split(":").Last();

        return cookieField;
    }
}
