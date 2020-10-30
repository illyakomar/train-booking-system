using Microsoft.EntityFrameworkCore.Migrations;

namespace train_booking.Migrations
{
    public partial class updatetableseat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Route_RouteId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Seat_SeatId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_RouteId",
                table: "Ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seat",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "SeatId",
                table: "Seat");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Seat",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seat",
                table: "Seat",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Seat_SeatId",
                table: "Ticket",
                column: "SeatId",
                principalTable: "Seat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Seat_SeatId",
                table: "Ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seat",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Seat");

            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SeatId",
                table: "Seat",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seat",
                table: "Seat",
                column: "SeatId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Seat_SeatId",
                table: "Ticket",
                column: "SeatId",
                principalTable: "Seat",
                principalColumn: "SeatId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
