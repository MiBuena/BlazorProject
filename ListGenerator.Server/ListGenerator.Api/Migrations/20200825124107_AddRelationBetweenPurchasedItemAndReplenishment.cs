using Microsoft.EntityFrameworkCore.Migrations;

namespace ListGenerator.Api.Migrations
{
    public partial class AddRelationBetweenPurchasedItemAndReplenishment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReplenishmentId",
                table: "PurchasedItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedItem_ReplenishmentId",
                table: "PurchasedItem",
                column: "ReplenishmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedItem_Replenishments_ReplenishmentId",
                table: "PurchasedItem",
                column: "ReplenishmentId",
                principalTable: "Replenishments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedItem_Replenishments_ReplenishmentId",
                table: "PurchasedItem");

            migrationBuilder.DropIndex(
                name: "IX_PurchasedItem_ReplenishmentId",
                table: "PurchasedItem");

            migrationBuilder.DropColumn(
                name: "ReplenishmentId",
                table: "PurchasedItem");
        }
    }
}
