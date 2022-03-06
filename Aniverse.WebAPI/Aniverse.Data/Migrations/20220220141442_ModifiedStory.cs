using Microsoft.EntityFrameworkCore.Migrations;

namespace Animalgram.Data.Migrations
{
    public partial class ModifiedStory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Story");

            migrationBuilder.AddColumn<string>(
                name: "StoryFileName",
                table: "Story",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoryFileName",
                table: "Story");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Story",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
