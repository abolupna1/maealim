using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace maealim.Migrations
{
    public partial class Notables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notables",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GuestReservationId = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: false),
                    JobNotableId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    MobileInMyCountry = table.Column<string>(nullable: false),
                    MobileInSaudi = table.Column<string>(nullable: true),
                    MGuideId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notables_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notables_GuestReservations_GuestReservationId",
                        column: x => x.GuestReservationId,
                        principalTable: "GuestReservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notables_JobNotable_JobNotableId",
                        column: x => x.JobNotableId,
                        principalTable: "JobNotable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notables_MGuides_MGuideId",
                        column: x => x.MGuideId,
                        principalTable: "MGuides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notables_CountryId",
                table: "Notables",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Notables_GuestReservationId",
                table: "Notables",
                column: "GuestReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Notables_JobNotableId",
                table: "Notables",
                column: "JobNotableId");

            migrationBuilder.CreateIndex(
                name: "IX_Notables_MGuideId",
                table: "Notables",
                column: "MGuideId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notables");
        }
    }
}
