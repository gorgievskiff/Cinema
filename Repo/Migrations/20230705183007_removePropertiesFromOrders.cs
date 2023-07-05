using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Data.Migrations
{
    public partial class removePropertiesFromOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartTickets_Orders_OrderId",
                table: "ShoppingCartTickets");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartTickets_OrderId",
                table: "ShoppingCartTickets");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ShoppingCartTickets");

            migrationBuilder.DropColumn(
                name: "ShoppingCartTicketsId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "ShoppingCartTickets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartTicketsId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartTickets_OrderId",
                table: "ShoppingCartTickets",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartTickets_Orders_OrderId",
                table: "ShoppingCartTickets",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
