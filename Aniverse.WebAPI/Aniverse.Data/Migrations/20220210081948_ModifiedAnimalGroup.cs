using Microsoft.EntityFrameworkCore.Migrations;

namespace Animalgram.Data.Migrations
{
    public partial class ModifiedAnimalGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalGroups_Animal_AnimalId1",
                table: "AnimalGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimalGroups_Groups_GroupId1",
                table: "AnimalGroups");

            migrationBuilder.DropIndex(
                name: "IX_AnimalGroups_AnimalId1",
                table: "AnimalGroups");

            migrationBuilder.DropIndex(
                name: "IX_AnimalGroups_GroupId1",
                table: "AnimalGroups");

            migrationBuilder.DropColumn(
                name: "AnimalId1",
                table: "AnimalGroups");

            migrationBuilder.DropColumn(
                name: "GroupId1",
                table: "AnimalGroups");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "AnimalGroups",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AnimalId",
                table: "AnimalGroups",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnimalGroups_AnimalId",
                table: "AnimalGroups",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalGroups_GroupId",
                table: "AnimalGroups",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalGroups_Animal_AnimalId",
                table: "AnimalGroups",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalGroups_Groups_GroupId",
                table: "AnimalGroups",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalGroups_Animal_AnimalId",
                table: "AnimalGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimalGroups_Groups_GroupId",
                table: "AnimalGroups");

            migrationBuilder.DropIndex(
                name: "IX_AnimalGroups_AnimalId",
                table: "AnimalGroups");

            migrationBuilder.DropIndex(
                name: "IX_AnimalGroups_GroupId",
                table: "AnimalGroups");

            migrationBuilder.AlterColumn<string>(
                name: "GroupId",
                table: "AnimalGroups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "AnimalId",
                table: "AnimalGroups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "AnimalId1",
                table: "AnimalGroups",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupId1",
                table: "AnimalGroups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnimalGroups_AnimalId1",
                table: "AnimalGroups",
                column: "AnimalId1");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalGroups_GroupId1",
                table: "AnimalGroups",
                column: "GroupId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalGroups_Animal_AnimalId1",
                table: "AnimalGroups",
                column: "AnimalId1",
                principalTable: "Animal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalGroups_Groups_GroupId1",
                table: "AnimalGroups",
                column: "GroupId1",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
