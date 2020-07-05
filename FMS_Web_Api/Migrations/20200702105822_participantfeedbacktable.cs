using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS_Web_Api.Migrations
{
    public partial class participantfeedbacktable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParticipantFeedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    ParticipantType = table.Column<string>(nullable: true),
                    QuestionId = table.Column<int>(nullable: false),
                    Answer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantFeedbacks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParticipantFeedbacks");
        }
    }
}
