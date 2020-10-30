using Microsoft.EntityFrameworkCore.Migrations;

namespace train_booking.Migrations
{
    public partial class UpdateRoutAndTrain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Route_TrainId",
                table: "Route");

            migrationBuilder.CreateIndex(
                name: "IX_Route_TrainId",
                table: "Route",
                column: "TrainId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Route_TrainId",
                table: "Route");

            migrationBuilder.CreateIndex(
                name: "IX_Route_TrainId",
                table: "Route",
                column: "TrainId");
        }
    }
}
