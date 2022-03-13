using Microsoft.EntityFrameworkCore.Migrations;

namespace Animalgram.Data.Migrations
{
    public partial class DropSavePost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SavePosts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SavePosts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreationDate = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "GETDATE()"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavePosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavePosts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SavePosts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SavePosts_PostId",
                table: "SavePosts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_SavePosts_UserId",
                table: "SavePosts",
                column: "UserId");
        }
    }
}
