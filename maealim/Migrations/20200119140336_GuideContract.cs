using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace maealim.Migrations
{
    public partial class GuideContract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GuideContracts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SeasonId = table.Column<int>(nullable: false),
                    GuideId = table.Column<int>(nullable: false),
                    JopId = table.Column<int>(nullable: false),
                    DailyWorkingHours = table.Column<int>(nullable: false),
                    HourlyPay = table.Column<float>(nullable: false),
                    Extra = table.Column<float>(nullable: false),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    GuideContractId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuideContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuideContracts_GuideContracts_GuideContractId",
                        column: x => x.GuideContractId,
                        principalTable: "GuideContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GuideContracts_MGuides_GuideId",
                        column: x => x.GuideId,
                        principalTable: "MGuides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GuideContracts_Jops_JopId",
                        column: x => x.JopId,
                        principalTable: "Jops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GuideContracts_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuideContracts_GuideContractId",
                table: "GuideContracts",
                column: "GuideContractId");

            migrationBuilder.CreateIndex(
                name: "IX_GuideContracts_GuideId",
                table: "GuideContracts",
                column: "GuideId");

            migrationBuilder.CreateIndex(
                name: "IX_GuideContracts_JopId",
                table: "GuideContracts",
                column: "JopId");

            migrationBuilder.CreateIndex(
                name: "IX_GuideContracts_SeasonId",
                table: "GuideContracts",
                column: "SeasonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuideContracts");
        }
    }
}
