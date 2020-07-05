using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS_Web_Api.Migrations
{
    public partial class participanttables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventNotParticipatedUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventNotParticipatedUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventParticipatedUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventParticipatedUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventUnregisteredUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventUnregisteredUsers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventNotParticipatedUsers");

            migrationBuilder.DropTable(
                name: "EventParticipatedUsers");

            migrationBuilder.DropTable(
                name: "EventUnregisteredUsers");
        }
    }
}
