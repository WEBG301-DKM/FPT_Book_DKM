using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShop1Asm.Migrations
{
    public partial class requestStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Request",
                newName: "StatusId");

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "Request",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "RequestStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatus", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "RequestStatus",
                columns: new[] { "Id", "Status" },
                values: new object[] { 1, "Pending" });

            migrationBuilder.InsertData(
                table: "RequestStatus",
                columns: new[] { "Id", "Status" },
                values: new object[] { 2, "Accept" });

            migrationBuilder.InsertData(
                table: "RequestStatus",
                columns: new[] { "Id", "Status" },
                values: new object[] { 3, "Deny" });

            migrationBuilder.CreateIndex(
                name: "IX_Request_StatusId",
                table: "Request",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_RequestStatus_StatusId",
                table: "Request",
                column: "StatusId",
                principalTable: "RequestStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_RequestStatus_StatusId",
                table: "Request");

            migrationBuilder.DropTable(
                name: "RequestStatus");

            migrationBuilder.DropIndex(
                name: "IX_Request_StatusId",
                table: "Request");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Request",
                newName: "Status");

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "Request",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
