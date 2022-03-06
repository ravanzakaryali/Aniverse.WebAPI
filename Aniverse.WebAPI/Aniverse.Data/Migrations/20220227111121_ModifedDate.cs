using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Animalgram.Data.Migrations
{
    public partial class ModifedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Picture_Animal_AnimalId1",
                table: "Picture");

            migrationBuilder.DropIndex(
                name: "IX_Picture_AnimalId1",
                table: "Picture");

            migrationBuilder.DropColumn(
                name: "AnimalId1",
                table: "Picture");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Story",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Posts",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<int>(
                name: "AnimalId",
                table: "Picture",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SenderDate",
                table: "Comments",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "AspNetUsers",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Picture_AnimalId",
                table: "Picture",
                column: "AnimalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_Animal_AnimalId",
                table: "Picture",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Picture_Animal_AnimalId",
                table: "Picture");

            migrationBuilder.DropIndex(
                name: "IX_Picture_AnimalId",
                table: "Picture");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Story",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "AnimalId",
                table: "Picture",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnimalId1",
                table: "Picture",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SenderDate",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.CreateIndex(
                name: "IX_Picture_AnimalId1",
                table: "Picture",
                column: "AnimalId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_Animal_AnimalId1",
                table: "Picture",
                column: "AnimalId1",
                principalTable: "Animal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
