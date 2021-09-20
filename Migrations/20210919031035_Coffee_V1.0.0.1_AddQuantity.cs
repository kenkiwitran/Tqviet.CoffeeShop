using Microsoft.EntityFrameworkCore.Migrations;

namespace Tqviet.CoffeeShop.Migrations
{
    public partial class Coffee_V1001_AddQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CoffeeOrderQuantity",
                table: "CoffeeOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoffeeOrderQuantity",
                table: "CoffeeOrders");
        }
    }
}
