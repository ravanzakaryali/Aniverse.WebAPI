using Microsoft.EntityFrameworkCore.Migrations;

namespace Animalgram.Data.Migrations
{
    public partial class ModifiedRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserSM_UserId",
                table: "UserSM");

            migrationBuilder.CreateIndex(
                name: "IX_UserSM_UserId",
                table: "UserSM",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserSM_UserId",
                table: "UserSM");

            migrationBuilder.CreateIndex(
                name: "IX_UserSM_UserId",
                table: "UserSM",
                column: "UserId");
        }
    }
}
