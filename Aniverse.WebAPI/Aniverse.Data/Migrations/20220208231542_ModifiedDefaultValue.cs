using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Animalgram.Data.Migrations
{
    public partial class ModifiedDefaultValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 8, 23, 15, 41, 832, DateTimeKind.Utc).AddTicks(3915),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<bool>(
                name: "IsBlock",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 2, 8, 23, 15, 41, 832, DateTimeKind.Utc).AddTicks(3915));

            migrationBuilder.AlterColumn<bool>(
                name: "IsBlock",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);
        }
    }
}
