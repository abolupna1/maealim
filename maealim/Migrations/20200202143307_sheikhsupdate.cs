using Microsoft.EntityFrameworkCore.Migrations;

namespace maealim.Migrations
{
    public partial class sheikhsupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Volunteer",
                table: "Sheikhs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Volunteer",
                table: "Sheikhs");
        }
    }
}
