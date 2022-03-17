using Microsoft.EntityFrameworkCore.Migrations;

namespace Animalgram.Data.Migrations
{
    public partial class AnimalPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Posts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAnimalCoverPicture",
                table: "Picture",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAnimalProfilePicture",
                table: "Picture",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsAnimalCoverPicture",
                table: "Picture");

            migrationBuilder.DropColumn(
                name: "IsAnimalProfilePicture",
                table: "Picture");
        }
    }
}
