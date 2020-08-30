using Microsoft.EntityFrameworkCore.Migrations;

namespace ListGenerator.Api.Migrations
{
    public partial class RemoveReplenishmentIdFromPurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReplenishmentId",
                table: "Purchases");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReplenishmentId",
                table: "Purchases",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
