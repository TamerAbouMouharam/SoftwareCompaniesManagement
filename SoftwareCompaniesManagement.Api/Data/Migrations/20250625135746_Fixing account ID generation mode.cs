using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareCompaniesManagement.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixingaccountIDgenerationmode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Accounts_AccountId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Developers_Accounts_AccountId",
                table: "Developers");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Accounts_AccountId",
                table: "Employees");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Accounts_TempId",
                table: "Accounts");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Accounts_TempId1",
                table: "Accounts");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Accounts_TempId2",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "TempId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "TempId1",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "TempId2",
                table: "Accounts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Accounts_AccountId",
                table: "Companies",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Developers_Accounts_AccountId",
                table: "Developers",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Accounts_AccountId",
                table: "Employees",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Accounts_AccountId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Developers_Accounts_AccountId",
                table: "Developers");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Accounts_AccountId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "TempId",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TempId1",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TempId2",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Accounts_TempId",
                table: "Accounts",
                column: "TempId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Accounts_TempId1",
                table: "Accounts",
                column: "TempId1");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Accounts_TempId2",
                table: "Accounts",
                column: "TempId2");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Accounts_AccountId",
                table: "Companies",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "TempId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Developers_Accounts_AccountId",
                table: "Developers",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "TempId1",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Accounts_AccountId",
                table: "Employees",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "TempId2",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
