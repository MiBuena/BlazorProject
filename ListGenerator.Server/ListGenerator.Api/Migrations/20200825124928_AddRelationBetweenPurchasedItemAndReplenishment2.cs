using Microsoft.EntityFrameworkCore.Migrations;

namespace ListGenerator.Api.Migrations
{
    public partial class AddRelationBetweenPurchasedItemAndReplenishment2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedItem_Items_ItemId",
                table: "PurchasedItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedItem_Replenishments_ReplenishmentId",
                table: "PurchasedItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchasedItem",
                table: "PurchasedItem");

            migrationBuilder.RenameTable(
                name: "PurchasedItem",
                newName: "PurchasedItems");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasedItem_ReplenishmentId",
                table: "PurchasedItems",
                newName: "IX_PurchasedItems_ReplenishmentId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasedItem_ItemId",
                table: "PurchasedItems",
                newName: "IX_PurchasedItems_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchasedItems",
                table: "PurchasedItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedItems_Items_ItemId",
                table: "PurchasedItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedItems_Replenishments_ReplenishmentId",
                table: "PurchasedItems",
                column: "ReplenishmentId",
                principalTable: "Replenishments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedItems_Items_ItemId",
                table: "PurchasedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedItems_Replenishments_ReplenishmentId",
                table: "PurchasedItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchasedItems",
                table: "PurchasedItems");

            migrationBuilder.RenameTable(
                name: "PurchasedItems",
                newName: "PurchasedItem");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasedItems_ReplenishmentId",
                table: "PurchasedItem",
                newName: "IX_PurchasedItem_ReplenishmentId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasedItems_ItemId",
                table: "PurchasedItem",
                newName: "IX_PurchasedItem_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchasedItem",
                table: "PurchasedItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedItem_Items_ItemId",
                table: "PurchasedItem",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedItem_Replenishments_ReplenishmentId",
                table: "PurchasedItem",
                column: "ReplenishmentId",
                principalTable: "Replenishments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
