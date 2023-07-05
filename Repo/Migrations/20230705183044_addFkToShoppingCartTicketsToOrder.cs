using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Data.Migrations
{
    public partial class addFkToShoppingCartTicketsToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "ShoppingCartTickets",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartTickets_OrderId",
                table: "ShoppingCartTickets",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartTickets_Orders_OrderId",
                table: "ShoppingCartTickets",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
