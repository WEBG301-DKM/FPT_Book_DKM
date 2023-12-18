using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShop1Asm.Migrations
{
    public partial class reasonForReequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "Request",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reason",
                table: "Request");
        }
    }
}
