using Microsoft.EntityFrameworkCore.Migrations;

namespace train_booking.Migrations
{
    public partial class UpdateTrainDriverAndDispatcher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Train_TrainDriver_TrainDriverId",
                table: "Train");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainDriver",
                table: "TrainDriver");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dispatcher",
                table: "Dispatcher");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TrainDriver");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Dispatcher");

            migrationBuilder.AddColumn<int>(
                name: "TrainDriverId",
                table: "TrainDriver",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "DispatcherId",
                table: "Dispatcher",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainDriver",
                table: "TrainDriver",
                column: "TrainDriverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dispatcher",
                table: "Dispatcher",
                column: "DispatcherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Train_TrainDriver_TrainDriverId",
                table: "Train",
                column: "TrainDriverId",
                principalTable: "TrainDriver",
                principalColumn: "TrainDriverId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Train_TrainDriver_TrainDriverId",
                table: "Train");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainDriver",
                table: "TrainDriver");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dispatcher",
                table: "Dispatcher");

            migrationBuilder.DropColumn(
                name: "TrainDriverId",
                table: "TrainDriver");

            migrationBuilder.DropColumn(
                name: "DispatcherId",
                table: "Dispatcher");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TrainDriver",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Dispatcher",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainDriver",
                table: "TrainDriver",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dispatcher",
                table: "Dispatcher",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Train_TrainDriver_TrainDriverId",
                table: "Train",
                column: "TrainDriverId",
                principalTable: "TrainDriver",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
