using Microsoft.EntityFrameworkCore.Migrations;

namespace train_booking.Migrations
{
    public partial class updatetableseat2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Seat_SeatId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_SeatId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "SeatId",
                table: "Ticket");

            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "Ticket",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_RouteId",
                table: "Ticket",
                column: "RouteId");

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

            migrationBuilder.DropIndex(
                name: "IX_Ticket_RouteId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "Ticket");

            migrationBuilder.AddColumn<int>(
                name: "SeatId",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_SeatId",
                table: "Ticket",
                column: "SeatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Seat_SeatId",
                table: "Ticket",
                column: "SeatId",
                principalTable: "Seat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
