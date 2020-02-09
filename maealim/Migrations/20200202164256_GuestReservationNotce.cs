using Microsoft.EntityFrameworkCore.Migrations;

namespace maealim.Migrations
{
    public partial class GuestReservationNotce : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notce",
                table: "GuestReservations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notce",
                table: "GuestReservations");
        }
    }
}
