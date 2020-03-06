using Microsoft.EntityFrameworkCore.Migrations;

namespace Phrases.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "phrase");

            migrationBuilder.CreateSequence(
                name: "phraseseq",
                schema: "phrase",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "phrases",
                schema: "phrase",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    MatchId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false),
                    Positive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phrases", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "phrases",
                schema: "phrase");

            migrationBuilder.DropSequence(
                name: "phraseseq",
                schema: "phrase");
        }
    }
}
