using Microsoft.EntityFrameworkCore.Migrations;

namespace Animalgram.Data.Migrations
{
    public partial class AnimalChangeColumb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Animal");

            migrationBuilder.AddColumn<string>(
                name: "Breed",
                table: "Animal",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Breed",
                table: "Animal");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Animal",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
