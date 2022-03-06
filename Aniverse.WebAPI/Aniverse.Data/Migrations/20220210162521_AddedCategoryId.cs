using Microsoft.EntityFrameworkCore.Migrations;

namespace Animalgram.Data.Migrations
{
    public partial class AddedCategoryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Picture_PostProducts_PostProductId",
                table: "Picture");

            migrationBuilder.DropIndex(
                name: "IX_Picture_PostProductId",
                table: "Picture");

            migrationBuilder.DropColumn(
                name: "PostProductId",
                table: "Picture");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "PostProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Picture",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostProducts_CategoryId",
                table: "PostProducts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Picture_ProductId",
                table: "Picture",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_PostProducts_ProductId",
                table: "Picture",
                column: "ProductId",
                principalTable: "PostProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostProducts_ProductCategory_CategoryId",
                table: "PostProducts",
                column: "CategoryId",
                principalTable: "ProductCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Picture_PostProducts_ProductId",
                table: "Picture");

            migrationBuilder.DropForeignKey(
                name: "FK_PostProducts_ProductCategory_CategoryId",
                table: "PostProducts");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropIndex(
                name: "IX_PostProducts_CategoryId",
                table: "PostProducts");

            migrationBuilder.DropIndex(
                name: "IX_Picture_ProductId",
                table: "Picture");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "PostProducts");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Picture");

            migrationBuilder.AddColumn<int>(
                name: "PostProductId",
                table: "Picture",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Picture_PostProductId",
                table: "Picture",
                column: "PostProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_PostProducts_PostProductId",
                table: "Picture",
                column: "PostProductId",
                principalTable: "PostProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
