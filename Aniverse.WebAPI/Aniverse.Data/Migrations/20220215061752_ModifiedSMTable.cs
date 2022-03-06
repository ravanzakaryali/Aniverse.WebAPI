using Microsoft.EntityFrameworkCore.Migrations;

namespace Animalgram.Data.Migrations
{
    public partial class ModifiedSMTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSocialMedia_AspNetUsers_UserId",
                table: "UserSocialMedia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSocialMedia",
                table: "UserSocialMedia");

            migrationBuilder.DropColumn(
                name: "IconName",
                table: "UserSocialMedia");

            migrationBuilder.DropColumn(
                name: "Sociallink",
                table: "UserSocialMedia");

            migrationBuilder.RenameTable(
                name: "UserSocialMedia",
                newName: "UserSM");

            migrationBuilder.RenameIndex(
                name: "IX_UserSocialMedia_UserId",
                table: "UserSM",
                newName: "IX_UserSM_UserId");

            migrationBuilder.AddColumn<string>(
                name: "IconClassName",
                table: "UserSM",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SmLink",
                table: "UserSM",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSM",
                table: "UserSM",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSM_AspNetUsers_UserId",
                table: "UserSM",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSM_AspNetUsers_UserId",
                table: "UserSM");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSM",
                table: "UserSM");

            migrationBuilder.DropColumn(
                name: "IconClassName",
                table: "UserSM");

            migrationBuilder.DropColumn(
                name: "SmLink",
                table: "UserSM");

            migrationBuilder.RenameTable(
                name: "UserSM",
                newName: "UserSocialMedia");

            migrationBuilder.RenameIndex(
                name: "IX_UserSM_UserId",
                table: "UserSocialMedia",
                newName: "IX_UserSocialMedia_UserId");

            migrationBuilder.AddColumn<string>(
                name: "IconName",
                table: "UserSocialMedia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sociallink",
                table: "UserSocialMedia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSocialMedia",
                table: "UserSocialMedia",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSocialMedia_AspNetUsers_UserId",
                table: "UserSocialMedia",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
