using Microsoft.EntityFrameworkCore.Migrations;

namespace train_booking.Migrations
{
    public partial class UpdateRoutAndTrain2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seat_AspNetUsers_UserId1",
                table: "Seat");

            migrationBuilder.DropIndex(
                name: "IX_Seat_UserId1",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Seat");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Seat",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Seat_UserId",
                table: "Seat",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_AspNetUsers_UserId",
                table: "Seat",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seat_AspNetUsers_UserId",
                table: "Seat");

            migrationBuilder.DropIndex(
                name: "IX_Seat_UserId",
                table: "Seat");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Seat",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Seat",
                type: "nvarchar(450)",
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
    }
}
