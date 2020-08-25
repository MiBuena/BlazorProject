using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ListGenerator.Api.Migrations
{
    public partial class AddNextReplenishmentDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "ReplenishmentPeriod",
                table: "Items",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "NextReplenishmentDate",
                table: "Items",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextReplenishmentDate",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "ReplenishmentPeriod",
                table: "Items",
                type: "int",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
