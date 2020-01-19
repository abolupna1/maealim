using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace maealim.Migrations
{
    public partial class MGuide : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MGuides",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountryId = table.Column<int>(nullable: true),
                    BankId = table.Column<int>(nullable: true),
                    UniversityId = table.Column<int>(nullable: true),
                    StageId = table.Column<int>(nullable: true),
                    LevelId = table.Column<int>(nullable: true),
                    LanguageId = table.Column<int>(nullable: true),
                    SpecializationId = table.Column<int>(nullable: true),
                    AppUserId = table.Column<int>(nullable: true),
                    CollegeId = table.Column<int>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MGuides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MGuides_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MGuides_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MGuides_Colleges_CollegeId",
                        column: x => x.CollegeId,
                        principalTable: "Colleges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MGuides_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MGuides_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MGuides_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MGuides_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MGuides_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MGuides_Universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Universities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MGuides_AppUserId",
                table: "MGuides",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MGuides_BankId",
                table: "MGuides",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_MGuides_CollegeId",
                table: "MGuides",
                column: "CollegeId");

            migrationBuilder.CreateIndex(
                name: "IX_MGuides_CountryId",
                table: "MGuides",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_MGuides_LanguageId",
                table: "MGuides",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_MGuides_LevelId",
                table: "MGuides",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_MGuides_SpecializationId",
                table: "MGuides",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_MGuides_StageId",
                table: "MGuides",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_MGuides_UniversityId",
                table: "MGuides",
                column: "UniversityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MGuides");
        }
    }
}
