namespace SoftwareCompaniesManagement.Api.MapEndpoints;

public static class Groups
{
    private static WebApplication app;

    public static RouteGroupBuilder Accounts { get; } = app.MapGroup("accounts");

    public static RouteGroupBuilder Companies { get; } = app.MapGroup("companies");

    public static RouteGroupBuilder Company { get; } = Companies.MapGroup("{companyId:int}");

    public static RouteGroupBuilder Developers { get; } = Company.MapGroup("developers");

    public static RouteGroupBuilder Employees { get; } = Company.MapGroup("employees");

    public static RouteGroupBuilder Projects { get; } = Company.MapGroup("projects");

    public static RouteGroupBuilder Project { get; } = Projects.MapGroup("{projectId:int}");

    public static RouteGroupBuilder Tasks { get; } = Project.MapGroup("tasks");

    public static RouteGroupBuilder Task { get; } = Tasks.MapGroup("{taskId:int}");

    public static void ProvideApp(this WebApplication app)
    {
        Groups.app = app;
    }
}
