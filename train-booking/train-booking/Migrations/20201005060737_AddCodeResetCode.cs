using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace train_booking.Migrations
{
    public partial class AddCodeResetCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastResetCodeCreationTime",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ResetCode",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastResetCodeCreationTime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ResetCode",
                table: "AspNetUsers");
        }
    }
}
