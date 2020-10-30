using Microsoft.EntityFrameworkCore.Migrations;

namespace train_booking.Migrations
{
    public partial class updateticket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Train_TrainId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_TrainId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "TrainId",
                table: "Ticket");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrainId",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TrainId",
                table: "Ticket",
                column: "TrainId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Train_TrainId",
                table: "Ticket",
                column: "TrainId",
                principalTable: "Train",
                principalColumn: "TrainId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
