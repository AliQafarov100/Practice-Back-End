using Microsoft.EntityFrameworkCore.Migrations;

namespace _6hours_Back_End.Migrations
{
    public partial class updateSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Settings",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Settings");

            migrationBuilder.RenameTable(
                name: "Settings",
                newName: "AnotherSettings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnotherSettings",
                table: "AnotherSettings",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AnotherSettings",
                table: "AnotherSettings");

            migrationBuilder.RenameTable(
                name: "AnotherSettings",
                newName: "Settings");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Settings",
                table: "Settings",
                column: "Id");
        }
    }
}
