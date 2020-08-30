using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ListGenerator.Api.Migrations
{
    public partial class AddReplenishmentDateToPurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Replenishments_ReplenishmentId",
                table: "Purchases");

            migrationBuilder.DropTable(
                name: "Replenishments");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_ReplenishmentId",
                table: "Purchases");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReplenishmentDate",
                table: "Purchases",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReplenishmentDate",
                table: "Purchases");

            migrationBuilder.CreateTable(
                name: "Replenishments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replenishments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_ReplenishmentId",
                table: "Purchases",
                column: "ReplenishmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Replenishments_ReplenishmentId",
                table: "Purchases",
                column: "ReplenishmentId",
                principalTable: "Replenishments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
