using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SoftwareCompaniesManagement.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class TechnologiesSeedUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Technologies",
                columns: new[] { "Id", "Description", "TechnologyName" },
                values: new object[,]
                {
                    { 5, "Frontend Development Library", "Angular" },
                    { 6, ".NET Technology For Building Windows Desktop Apps", "WinForms" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Technologies",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Technologies",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
