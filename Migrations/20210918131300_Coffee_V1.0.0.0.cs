using Microsoft.EntityFrameworkCore.Migrations;

namespace Tqviet.CoffeeShop.Migrations
{
    public partial class Coffee_V1000 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoffeeOrders",
                columns: table => new
                {
                    CoffeeOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoffeeOrderDateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoffeeOrderClientIp = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoffeeOrders", x => x.CoffeeOrderId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoffeeOrders");
        }
    }
}
