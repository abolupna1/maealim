using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace maealim.Migrations
{
    public partial class Attends : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attends",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: true),
                    EmployeeContractId = table.Column<int>(nullable: true),
                    GuideId = table.Column<int>(nullable: true),
                    GuideContractId = table.Column<int>(nullable: true),
                    AttendDate = table.Column<DateTime>(nullable: false),
                    TheWork = table.Column<string>(nullable: false),
                    AppUserId = table.Column<int>(nullable: true),
                    WorkingHours = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attends_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attends_EmployeeContracts_EmployeeContractId",
                        column: x => x.EmployeeContractId,
                        principalTable: "EmployeeContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attends_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attends_GuideContracts_GuideContractId",
                        column: x => x.GuideContractId,
                        principalTable: "GuideContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attends_MGuides_GuideId",
                        column: x => x.GuideId,
                        principalTable: "MGuides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attends_AppUserId",
                table: "Attends",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Attends_EmployeeContractId",
                table: "Attends",
                column: "EmployeeContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Attends_EmployeeId",
                table: "Attends",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Attends_GuideContractId",
                table: "Attends",
                column: "GuideContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Attends_GuideId",
                table: "Attends",
                column: "GuideId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attends");
        }
    }
}
