using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareCompaniesManagement.Authorization.Data.Migrations
{
    /// <inheritdoc />
    public partial class InfoIdAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InfoId",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InfoId",
                table: "Accounts");
        }
    }
}
