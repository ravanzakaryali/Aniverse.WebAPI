using Microsoft.EntityFrameworkCore.Migrations;

namespace Animalgram.Data.Migrations
{
    public partial class AddedUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_ReceiverId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Picture_AspNetUsers_AppUserId",
                table: "Picture");

            migrationBuilder.DropIndex(
                name: "IX_Picture_AppUserId",
                table: "Picture");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_SenderId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Picture");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Picture",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MessageGroupId",
                table: "Messages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MessageGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<string>(nullable: true),
                    ReceiverId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageGroup_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageGroup_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Picture_UserId",
                table: "Picture",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AppUserId",
                table: "Messages",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AppUserId1",
                table: "Messages",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MessageGroupId",
                table: "Messages",
                column: "MessageGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageGroup_ReceiverId",
                table: "MessageGroup",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageGroup_SenderId",
                table: "MessageGroup",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_AppUserId",
                table: "Messages",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_AppUserId1",
                table: "Messages",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MessageGroup_MessageGroupId",
                table: "Messages",
                column: "MessageGroupId",
                principalTable: "MessageGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_AspNetUsers_UserId",
                table: "Picture",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_AppUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_AppUserId1",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MessageGroup_MessageGroupId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Picture_AspNetUsers_UserId",
                table: "Picture");

            migrationBuilder.DropTable(
                name: "MessageGroup");

            migrationBuilder.DropIndex(
                name: "IX_Picture_UserId",
                table: "Picture");

            migrationBuilder.DropIndex(
                name: "IX_Messages_AppUserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_AppUserId1",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_MessageGroupId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Picture");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "MessageGroupId",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Picture",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiverId",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderId",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Picture_AppUserId",
                table: "Picture",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_ReceiverId",
                table: "Messages",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_AspNetUsers_AppUserId",
                table: "Picture",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
