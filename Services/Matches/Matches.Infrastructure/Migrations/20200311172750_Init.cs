using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Matches.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                "match");

            migrationBuilder.CreateSequence(
                "matchseq",
                "match",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                "teamseq",
                "match",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                "matches",
                schema: "match",
                columns: table => new
                {
                    Id = table.Column<int>(),
                    AwayTeamId = table.Column<int>(),
                    HomeTeamId = table.Column<int>(),
                    StatusId = table.Column<int>(),
                    UtcDate = table.Column<DateTime>(),
                    ScoreWinner = table.Column<string>(nullable: true),
                    ScoreHomeTeam = table.Column<int>(nullable: true),
                    ScoreAwayTeam = table.Column<int>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_matches", x => x.Id); });

            migrationBuilder.CreateTable(
                "teams",
                schema: "match",
                columns: table => new
                {
                    Id = table.Column<int>(),
                    Name = table.Column<string>()
                },
                constraints: table => { table.PrimaryKey("PK_teams", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "matches",
                "match");

            migrationBuilder.DropTable(
                "teams",
                "match");

            migrationBuilder.DropSequence(
                "matchseq",
                "match");

            migrationBuilder.DropSequence(
                "teamseq",
                "match");
        }
    }
}