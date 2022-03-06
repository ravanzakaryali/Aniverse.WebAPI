using Microsoft.EntityFrameworkCore.Migrations;

namespace Animalgram.Data.Migrations
{
    public partial class AddCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdModified",
                table: "Comments");

            migrationBuilder.AddColumn<bool>(
                name: "IsModified",
                table: "Comments",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsModified",
                table: "Comments");

            migrationBuilder.AddColumn<bool>(
                name: "IdModified",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
