using Microsoft.EntityFrameworkCore.Migrations;

namespace Animalgram.Data.Migrations
{
    public partial class ModifiedPageFollow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PageFollows_PageId",
                table: "PageFollows");

            migrationBuilder.CreateIndex(
                name: "IX_PageFollows_PageId",
                table: "PageFollows",
                column: "PageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PageFollows_PageId",
                table: "PageFollows");

            migrationBuilder.CreateIndex(
                name: "IX_PageFollows_PageId",
                table: "PageFollows",
                column: "PageId",
                unique: false);
        }
    }
}
