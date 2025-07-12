using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SoftwareCompaniesManagement.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataAddedForTests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Technologies",
                columns: new[] { "Id", "Description", "TechnologyName" },
                values: new object[,]
                {
                    { 1, "RDBMS", "Microsoft SQL Server" },
                    { 2, "Frontend Development Library", "React" },
                    { 3, "Backend Development Tools Uisng C#", "ASP.NET Core" },
                    { 4, "A Cross-Platform App Development Framework Using C#", ".NET MAUI" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Technologies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Technologies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Technologies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Technologies",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
