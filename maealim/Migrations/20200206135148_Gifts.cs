using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace maealim.Migrations
{
    public partial class Gifts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gifts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemOfProductId = table.Column<int>(nullable: false),
                    GuestReservationId = table.Column<int>(nullable: false),
                    Qty = table.Column<int>(nullable: false),
                    Notce = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gifts_GuestReservations_GuestReservationId",
                        column: x => x.GuestReservationId,
                        principalTable: "GuestReservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gifts_ItemOfProducts_ItemOfProductId",
                        column: x => x.ItemOfProductId,
                        principalTable: "ItemOfProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gifts_GuestReservationId",
                table: "Gifts",
                column: "GuestReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Gifts_ItemOfProductId",
                table: "Gifts",
                column: "ItemOfProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gifts");
        }
    }
}
