using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace maealim.Migrations
{
    public partial class WjhaaMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WjhaaMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypesMessageId = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: false),
                    MGuideId = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WjhaaMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WjhaaMessages_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WjhaaMessages_MGuides_MGuideId",
                        column: x => x.MGuideId,
                        principalTable: "MGuides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WjhaaMessages_TypesMessages_TypesMessageId",
                        column: x => x.TypesMessageId,
                        principalTable: "TypesMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WjhaaMessages_CountryId",
                table: "WjhaaMessages",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_WjhaaMessages_MGuideId",
                table: "WjhaaMessages",
                column: "MGuideId");

            migrationBuilder.CreateIndex(
                name: "IX_WjhaaMessages_TypesMessageId",
                table: "WjhaaMessages",
                column: "TypesMessageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WjhaaMessages");
        }
    }
}
