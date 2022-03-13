using Microsoft.EntityFrameworkCore.Migrations;

namespace Animalgram.Data.Migrations
{
    public partial class CreateSavePost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SavePosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<string>(nullable: true, defaultValueSql: "GETDATE()"),
                    UserId = table.Column<string>(nullable: false),
                    PostId = table.Column<int>(nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SavePosts");
        }
    }
}
