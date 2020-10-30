using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace train_booking.Migrations
{
    public partial class AddRouteTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "Ticket",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    SaleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    TicketId = table.Column<int>(nullable: false),
                    SaleDate = table.Column<DateTime>(nullable: false),
                    UserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.SaleId);
                    table.ForeignKey(
                        name: "FK_Sale_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sale_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_RouteId",
                table: "Ticket",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_TicketId",
                table: "Sale",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_UserId1",
                table: "Sale",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Route_RouteId",
                table: "Ticket",
                column: "RouteId",
                principalTable: "Route",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Route_RouteId",
                table: "Ticket");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_RouteId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "Ticket");
        }
    }
}
