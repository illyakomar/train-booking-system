using Microsoft.EntityFrameworkCore.Migrations;

namespace train_booking.Migrations
{
    public partial class addSeat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeatId",
                table: "Ticket",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Seat",
                columns: table => new
                {
                    SeatId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WagonId = table.Column<int>(nullable: false),
                    SeatNumber = table.Column<int>(nullable: false),
                    SeatAvailability = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seat", x => x.SeatId);
                    table.ForeignKey(
                        name: "FK_Seat_Wagon_WagonId",
                        column: x => x.WagonId,
                        principalTable: "Wagon",
                        principalColumn: "WagonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_SeatId",
                table: "Ticket",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Seat_WagonId",
                table: "Seat",
                column: "WagonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Seat_SeatId",
                table: "Ticket",
                column: "SeatId",
                principalTable: "Seat",
                principalColumn: "SeatId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Seat_SeatId",
                table: "Ticket");

            migrationBuilder.DropTable(
                name: "Seat");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_SeatId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "SeatId",
                table: "Ticket");
        }
    }
}
