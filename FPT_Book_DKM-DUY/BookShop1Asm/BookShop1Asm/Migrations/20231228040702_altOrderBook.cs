using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShop1Asm.Migrations
{
    public partial class altOrderBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "BookPrice",
                table: "OrderBook",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookPrice",
                table: "OrderBook");
        }
    }
}
