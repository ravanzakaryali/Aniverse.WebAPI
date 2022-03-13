using Microsoft.EntityFrameworkCore.Migrations;

namespace Animalgram.Data.Migrations
{
    public partial class SavePosrIdModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavePosts_Posts_PostId1",
                table: "SavePosts");

            migrationBuilder.DropIndex(
                name: "IX_SavePosts_PostId1",
                table: "SavePosts");

            migrationBuilder.DropColumn(
                name: "PostId1",
                table: "SavePosts");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "SavePosts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SavePosts_PostId",
                table: "SavePosts",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavePosts_Posts_PostId",
                table: "SavePosts",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavePosts_Posts_PostId",
                table: "SavePosts");

            migrationBuilder.DropIndex(
                name: "IX_SavePosts_PostId",
                table: "SavePosts");

            migrationBuilder.AlterColumn<string>(
                name: "PostId",
                table: "SavePosts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PostId1",
                table: "SavePosts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SavePosts_PostId1",
                table: "SavePosts",
                column: "PostId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SavePosts_Posts_PostId1",
                table: "SavePosts",
                column: "PostId1",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
