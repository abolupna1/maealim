using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace maealim.Migrations
{
    public partial class Guide : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guides",
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
                    table.PrimaryKey("PK_Guides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guides_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Guides_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Guides_Colleges_CollegeId",
                        column: x => x.CollegeId,
                        principalTable: "Colleges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Guides_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Guides_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Guides_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Guides_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Guides_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Guides_Universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Universities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Guides_AppUserId",
                table: "Guides",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Guides_BankId",
                table: "Guides",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Guides_CollegeId",
                table: "Guides",
                column: "CollegeId");

            migrationBuilder.CreateIndex(
                name: "IX_Guides_CountryId",
                table: "Guides",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Guides_LanguageId",
                table: "Guides",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Guides_LevelId",
                table: "Guides",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Guides_SpecializationId",
                table: "Guides",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_Guides_StageId",
                table: "Guides",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_Guides_UniversityId",
                table: "Guides",
                column: "UniversityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guides");
        }
    }
}
