using Microsoft.EntityFrameworkCore.Migrations;

namespace maealim.Migrations
{
    public partial class EmployeeContractUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "EmployeeContracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContracts_EmployeeId",
                table: "EmployeeContracts",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeContracts_Employees_EmployeeId",
                table: "EmployeeContracts",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeContracts_Employees_EmployeeId",
                table: "EmployeeContracts");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeContracts_EmployeeId",
                table: "EmployeeContracts");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "EmployeeContracts");
        }
    }
}
