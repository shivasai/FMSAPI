using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS_Web_Api.Migrations
{
    public partial class feedbacktablesadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeedbackQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionTye = table.Column<string>(nullable: true),
                    Question = table.Column<string>(nullable: true),
                    ParticipantType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackQuestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeedbackOptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(nullable: false),
                    Option = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedbackOptions_FeedbackQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "FeedbackQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackOptions_QuestionId",
                table: "FeedbackOptions",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedbackOptions");

            migrationBuilder.DropTable(
                name: "FeedbackQuestions");
        }
    }
}
