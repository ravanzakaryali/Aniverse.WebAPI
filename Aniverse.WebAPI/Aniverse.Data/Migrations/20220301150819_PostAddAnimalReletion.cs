using Microsoft.EntityFrameworkCore.Migrations;

namespace Animalgram.Data.Migrations
{
    public partial class PostAddAnimalReletion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnimalId",
                table: "Posts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AnimalId",
                table: "Posts",
                column: "AnimalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Animal_AnimalId",
                table: "Posts",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Animal_AnimalId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_AnimalId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "AnimalId",
                table: "Posts");
        }
    }
}
