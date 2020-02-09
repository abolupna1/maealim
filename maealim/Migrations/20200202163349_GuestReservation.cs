using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace maealim.Migrations
{
    public partial class GuestReservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GuestReservations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MGuideId = table.Column<int>(nullable: true),
                    SheikhId = table.Column<int>(nullable: true),
                    ReservationDate = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<string>(nullable: true),
                    SessionNo = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestReservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuestReservations_MGuides_MGuideId",
                        column: x => x.MGuideId,
                        principalTable: "MGuides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GuestReservations_Sheikhs_SheikhId",
                        column: x => x.SheikhId,
                        principalTable: "Sheikhs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuestReservations_MGuideId",
                table: "GuestReservations",
                column: "MGuideId");

            migrationBuilder.CreateIndex(
                name: "IX_GuestReservations_SheikhId",
                table: "GuestReservations",
                column: "SheikhId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuestReservations");
        }
    }
}
