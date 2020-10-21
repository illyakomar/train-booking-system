using Microsoft.EntityFrameworkCore.Migrations;

namespace train_booking.Migrations
{
    public partial class AddTableWagon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Train_TrainId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Train_Route_RouteId",
                table: "Train");

            migrationBuilder.DropForeignKey(
                name: "FK_Train_TrainDriver_TrainDriverId",
                table: "Train");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Train",
                table: "Train");

            migrationBuilder.DropIndex(
                name: "IX_Train_RouteId",
                table: "Train");

            migrationBuilder.DropIndex(
                name: "IX_Train_TrainDriverId",
                table: "Train");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Train");

            migrationBuilder.DropColumn(
                name: "PlaceCount",
                table: "Train");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "Train");

            migrationBuilder.DropColumn(
                name: "TrainDriverId",
                table: "Train");

            migrationBuilder.DropColumn(
                name: "TypeWagon",
                table: "Train");

            migrationBuilder.DropColumn(
                name: "WagonsCount",
                table: "Train");

            migrationBuilder.AddColumn<int>(
                name: "TrainId",
                table: "Train",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "TrainDriverId",
                table: "Route",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TrainId",
                table: "Route",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Train",
                table: "Train",
                column: "TrainId");

            migrationBuilder.CreateTable(
                name: "Wagon",
                columns: table => new
                {
                    WagonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainId = table.Column<int>(nullable: false),
                    TypeWagon = table.Column<int>(nullable: false),
                    PlaceCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wagon", x => x.WagonId);
                    table.ForeignKey(
                        name: "FK_Wagon_Train_TrainId",
                        column: x => x.TrainId,
                        principalTable: "Train",
                        principalColumn: "TrainId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Route_TrainDriverId",
                table: "Route",
                column: "TrainDriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Route_TrainId",
                table: "Route",
                column: "TrainId");

            migrationBuilder.CreateIndex(
                name: "IX_Wagon_TrainId",
                table: "Wagon",
                column: "TrainId");

            migrationBuilder.AddForeignKey(
                name: "FK_Route_TrainDriver_TrainDriverId",
                table: "Route",
                column: "TrainDriverId",
                principalTable: "TrainDriver",
                principalColumn: "TrainDriverId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Route_Train_TrainId",
                table: "Route",
                column: "TrainId",
                principalTable: "Train",
                principalColumn: "TrainId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Train_TrainId",
                table: "Ticket",
                column: "TrainId",
                principalTable: "Train",
                principalColumn: "TrainId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Route_TrainDriver_TrainDriverId",
                table: "Route");

            migrationBuilder.DropForeignKey(
                name: "FK_Route_Train_TrainId",
                table: "Route");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Train_TrainId",
                table: "Ticket");

            migrationBuilder.DropTable(
                name: "Wagon");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Train",
                table: "Train");

            migrationBuilder.DropIndex(
                name: "IX_Route_TrainDriverId",
                table: "Route");

            migrationBuilder.DropIndex(
                name: "IX_Route_TrainId",
                table: "Route");

            migrationBuilder.DropColumn(
                name: "TrainId",
                table: "Train");

            migrationBuilder.DropColumn(
                name: "TrainDriverId",
                table: "Route");

            migrationBuilder.DropColumn(
                name: "TrainId",
                table: "Route");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Train",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PlaceCount",
                table: "Train",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "Train",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TrainDriverId",
                table: "Train",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TypeWagon",
                table: "Train",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WagonsCount",
                table: "Train",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Train",
                table: "Train",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Train_RouteId",
                table: "Train",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Train_TrainDriverId",
                table: "Train",
                column: "TrainDriverId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Train_TrainId",
                table: "Ticket",
                column: "TrainId",
                principalTable: "Train",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Train_Route_RouteId",
                table: "Train",
                column: "RouteId",
                principalTable: "Route",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Train_TrainDriver_TrainDriverId",
                table: "Train",
                column: "TrainDriverId",
                principalTable: "TrainDriver",
                principalColumn: "TrainDriverId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
