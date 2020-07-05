using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS_Web_Api.Migrations
{
    public partial class schemachange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PocContactNum",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "PocId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "PocName",
                table: "Events");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PocContactNum",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PocId",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PocName",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
