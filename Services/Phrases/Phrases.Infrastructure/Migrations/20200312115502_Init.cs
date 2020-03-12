using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Phrases.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                "phrase");

            migrationBuilder.CreateSequence(
                "phraseseq",
                "phrase",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                "phrases",
                schema: "phrase",
                columns: table => new
                {
                    Id = table.Column<int>(),
                    DateCreated = table.Column<DateTime>(),
                    Description = table.Column<string>(),
                    MatchId = table.Column<int>(),
                    Positive = table.Column<bool>(),
                    TeamId = table.Column<int>()
                },
                constraints: table => { table.PrimaryKey("PK_phrases", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "phrases",
                "phrase");

            migrationBuilder.DropSequence(
                "phraseseq",
                "phrase");
        }
    }
}