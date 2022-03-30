using Microsoft.EntityFrameworkCore.Migrations;

namespace Animalgram.Data.Migrations
{
    public partial class OnDeleteCascadePicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Picture_Animal_AnimalId",
                table: "Picture");

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_Animal_AnimalId",
                table: "Picture",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Picture_Animal_AnimalId",
                table: "Picture");

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_Animal_AnimalId",
                table: "Picture",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
