using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Animalgram.Data.Migrations
{
    public partial class ModifiedAnimaltable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_AspNetUsers_UserId1",
                table: "Animal");

            migrationBuilder.DropIndex(
                name: "IX_Animal_UserId1",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Animal");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValueSql: "GetUtcDate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 8, 23, 15, 41, 832, DateTimeKind.Utc).AddTicks(3915));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Animal",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Animal_UserId",
                table: "Animal",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_AspNetUsers_UserId",
                table: "Animal",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_AspNetUsers_UserId",
                table: "Animal");

            migrationBuilder.DropIndex(
                name: "IX_Animal_UserId",
                table: "Animal");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 8, 23, 15, 41, 832, DateTimeKind.Utc).AddTicks(3915),
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GetUtcDate()");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Animal",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Animal",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animal_UserId1",
                table: "Animal",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_AspNetUsers_UserId1",
                table: "Animal",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
