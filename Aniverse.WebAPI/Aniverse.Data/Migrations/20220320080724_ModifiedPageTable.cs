using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Animalgram.Data.Migrations
{
    public partial class ModifiedPageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageFollow_Page_PageId",
                table: "PageFollow");

            migrationBuilder.DropForeignKey(
                name: "FK_PageFollow_AspNetUsers_UserId",
                table: "PageFollow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PageFollow",
                table: "PageFollow");

            migrationBuilder.RenameTable(
                name: "PageFollow",
                newName: "PageFollows");

            migrationBuilder.RenameIndex(
                name: "IX_PageFollow_UserId",
                table: "PageFollows",
                newName: "IX_PageFollows_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PageFollow_PageId",
                table: "PageFollows",
                newName: "IX_PageFollows_PageId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FollowDate",
                table: "PageFollows",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PageFollows",
                table: "PageFollows",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PageFollows_Page_PageId",
                table: "PageFollows",
                column: "PageId",
                principalTable: "Page",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PageFollows_AspNetUsers_UserId",
                table: "PageFollows",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageFollows_Page_PageId",
                table: "PageFollows");

            migrationBuilder.DropForeignKey(
                name: "FK_PageFollows_AspNetUsers_UserId",
                table: "PageFollows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PageFollows",
                table: "PageFollows");

            migrationBuilder.RenameTable(
                name: "PageFollows",
                newName: "PageFollow");

            migrationBuilder.RenameIndex(
                name: "IX_PageFollows_UserId",
                table: "PageFollow",
                newName: "IX_PageFollow_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PageFollows_PageId",
                table: "PageFollow",
                newName: "IX_PageFollow_PageId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FollowDate",
                table: "PageFollow",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PageFollow",
                table: "PageFollow",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PageFollow_Page_PageId",
                table: "PageFollow",
                column: "PageId",
                principalTable: "Page",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PageFollow_AspNetUsers_UserId",
                table: "PageFollow",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
