using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Animalgram.Data.Migrations
{
    public partial class AddedPageModal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PageId",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPageCoverPicture",
                table: "Picture",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPageProfilePicture",
                table: "Picture",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PageId",
                table: "Picture",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Page",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    About = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    BusinessName = table.Column<string>(nullable: true),
                    IsOfficial = table.Column<bool>(nullable: false, defaultValue: false),
                    UserId = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Page_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PageId",
                table: "Posts",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_Picture_PageId",
                table: "Picture",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_Page_UserId",
                table: "Page",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_Page_PageId",
                table: "Picture",
                column: "PageId",
                principalTable: "Page",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Page_PageId",
                table: "Posts",
                column: "PageId",
                principalTable: "Page",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Picture_Page_PageId",
                table: "Picture");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Page_PageId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "Page");

            migrationBuilder.DropIndex(
                name: "IX_Posts_PageId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Picture_PageId",
                table: "Picture");

            migrationBuilder.DropColumn(
                name: "PageId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsPageCoverPicture",
                table: "Picture");

            migrationBuilder.DropColumn(
                name: "IsPageProfilePicture",
                table: "Picture");

            migrationBuilder.DropColumn(
                name: "PageId",
                table: "Picture");
        }
    }
}
