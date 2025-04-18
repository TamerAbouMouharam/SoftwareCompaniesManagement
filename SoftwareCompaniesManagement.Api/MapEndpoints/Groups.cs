namespace SoftwareCompaniesManagement.Api.MapEndpoints;

public static class Groups
{
    private static WebApplication app;

    public static RouteGroupBuilder Accounts { get; } = app.MapGroup("accounts");

    public static RouteGroupBuilder Companies { get; } = app.MapGroup("companies");

    public static RouteGroupBuilder Company { get; } = Companies.MapGroup("{companyId}");

    public static RouteGroupBuilder Developers { get; } = Company.MapGroup("developers");

    public static RouteGroupBuilder Employees { get; } = Company.MapGroup("employees");

    public static RouteGroupBuilder Projects { get; } = Company.MapGroup("projects");

    public static RouteGroupBuilder Tasks { get; } = Projects.MapGroup("{projectId}/tasks");

    public static void ProvideApp(this WebApplication app)
    {
        Groups.app = app;
    }
}
