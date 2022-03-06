using Microsoft.EntityFrameworkCore.Migrations;

namespace Animalgram.Data.Migrations
{
    public partial class ChangeStory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stories_AspNetUsers_UserId",
                table: "Stories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stories",
                table: "Stories");

            migrationBuilder.RenameTable(
                name: "Stories",
                newName: "Story");

            migrationBuilder.RenameIndex(
                name: "IX_Stories_UserId",
                table: "Story",
                newName: "IX_Story_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Story",
                table: "Story",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Story_AspNetUsers_UserId",
                table: "Story",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Story_AspNetUsers_UserId",
                table: "Story");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Story",
                table: "Story");

            migrationBuilder.RenameTable(
                name: "Story",
                newName: "Stories");

            migrationBuilder.RenameIndex(
                name: "IX_Story_UserId",
                table: "Stories",
                newName: "IX_Stories_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stories",
                table: "Stories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_AspNetUsers_UserId",
                table: "Stories",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
