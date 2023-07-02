using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Data.Migrations
{
    public partial class AddAvaliableToTIcket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Tickets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Tickets");
        }
    }
}
