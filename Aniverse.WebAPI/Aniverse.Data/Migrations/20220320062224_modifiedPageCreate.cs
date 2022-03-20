using Microsoft.EntityFrameworkCore.Migrations;

namespace Animalgram.Data.Migrations
{
    public partial class modifiedPageCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessName",
                table: "Page");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Page",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pagename",
                table: "Page",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Page");

            migrationBuilder.DropColumn(
                name: "Pagename",
                table: "Page");

            migrationBuilder.AddColumn<string>(
                name: "BusinessName",
                table: "Page",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
