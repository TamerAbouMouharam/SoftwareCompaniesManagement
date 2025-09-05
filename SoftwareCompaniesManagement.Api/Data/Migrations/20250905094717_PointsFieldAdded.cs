using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareCompaniesManagement.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class PointsFieldAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Points",
                table: "Developers",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Points",
                table: "Developers");
        }
    }
}
