using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Data.Migrations
{
    public partial class DuplicateFkRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartTickets_Tickets_TicketId1",
                table: "ShoppingCartTickets");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartTickets_TicketId1",
                table: "ShoppingCartTickets");

            migrationBuilder.DropColumn(
                name: "TicketId1",
                table: "ShoppingCartTickets");

            migrationBuilder.AlterColumn<Guid>(
                name: "TicketId",
                table: "ShoppingCartTickets",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartTickets_TicketId",
                table: "ShoppingCartTickets",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartTickets_Tickets_TicketId",
                table: "ShoppingCartTickets",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartTickets_Tickets_TicketId",
                table: "ShoppingCartTickets");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartTickets_TicketId",
                table: "ShoppingCartTickets");

            migrationBuilder.AlterColumn<string>(
                name: "TicketId",
                table: "ShoppingCartTickets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "TicketId1",
                table: "ShoppingCartTickets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartTickets_TicketId1",
                table: "ShoppingCartTickets",
                column: "TicketId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartTickets_Tickets_TicketId1",
                table: "ShoppingCartTickets",
                column: "TicketId1",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
