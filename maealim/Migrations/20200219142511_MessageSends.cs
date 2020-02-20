using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace maealim.Migrations
{
    public partial class MessageSends : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessageSends",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NotableId = table.Column<int>(nullable: false),
                    WjhaaMessageId = table.Column<int>(nullable: false),
                    AppUserId = table.Column<int>(nullable: false),
                    SendDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageSends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageSends_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageSends_Notables_NotableId",
                        column: x => x.NotableId,
                        principalTable: "Notables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageSends_WjhaaMessages_WjhaaMessageId",
                        column: x => x.WjhaaMessageId,
                        principalTable: "WjhaaMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageSends_AppUserId",
                table: "MessageSends",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageSends_NotableId",
                table: "MessageSends",
                column: "NotableId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageSends_WjhaaMessageId",
                table: "MessageSends",
                column: "WjhaaMessageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageSends");
        }
    }
}
