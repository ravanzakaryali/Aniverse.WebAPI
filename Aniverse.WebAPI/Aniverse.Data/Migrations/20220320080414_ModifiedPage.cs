using Microsoft.EntityFrameworkCore.Migrations;

namespace Animalgram.Data.Migrations
{
    public partial class ModifiedPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageFollow_AspNetUsers_UserId1",
                table: "PageFollow");

            migrationBuilder.DropIndex(
                name: "IX_PageFollow_UserId1",
                table: "PageFollow");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "PageFollow");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "PageFollow",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_PageFollow_UserId",
                table: "PageFollow",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PageFollow_AspNetUsers_UserId",
                table: "PageFollow",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageFollow_AspNetUsers_UserId",
                table: "PageFollow");

            migrationBuilder.DropIndex(
                name: "IX_PageFollow_UserId",
                table: "PageFollow");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "PageFollow",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "PageFollow",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PageFollow_UserId1",
                table: "PageFollow",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PageFollow_AspNetUsers_UserId1",
                table: "PageFollow",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
