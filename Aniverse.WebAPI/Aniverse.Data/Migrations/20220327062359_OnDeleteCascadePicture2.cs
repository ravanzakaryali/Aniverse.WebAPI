using Microsoft.EntityFrameworkCore.Migrations;

namespace Animalgram.Data.Migrations
{
    public partial class OnDeleteCascadePicture2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Animal_AnimalId",
                table: "Posts");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Animal_AnimalId",
                table: "Posts",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Animal_AnimalId",
                table: "Posts");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Animal_AnimalId",
                table: "Posts",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
