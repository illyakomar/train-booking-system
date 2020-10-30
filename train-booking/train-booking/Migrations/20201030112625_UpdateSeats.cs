using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace train_booking.Migrations
{
    public partial class UpdateSeats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastBookingTime",
                table: "Seat",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Seat",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Seat",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seat_UserId1",
                table: "Seat",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_AspNetUsers_UserId1",
                table: "Seat",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seat_AspNetUsers_UserId1",
                table: "Seat");

            migrationBuilder.DropIndex(
                name: "IX_Seat_UserId1",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "LastBookingTime",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Seat");

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdditionalServices = table.Column<bool>(type: "bit", nullable: false),
                    Benefits = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlaceNumber = table.Column<int>(type: "int", nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_Route_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Route",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                name: "IX_Sale_TicketId",
                table: "Sale",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_UserId1",
                table: "Sale",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_RouteId",
                table: "Ticket",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_UserId1",
                table: "Ticket",
                column: "UserId1");
        }
    }
}
