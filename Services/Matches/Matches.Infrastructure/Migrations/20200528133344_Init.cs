using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Matches.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "match");

            migrationBuilder.CreateTable(
                name: "matches",
                schema: "match",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AwayTeamId = table.Column<Guid>(nullable: false),
                    HomeTeamId = table.Column<Guid>(nullable: false),
                    StatusId = table.Column<Guid>(nullable: false),
                    UtcDate = table.Column<DateTime>(nullable: false),
                    ScoreWinner = table.Column<string>(nullable: true),
                    ScoreHomeTeam = table.Column<int>(nullable: true),
                    ScoreAwayTeam = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "teams",
                schema: "match",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teams", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "matches",
                schema: "match");

            migrationBuilder.DropTable(
                name: "teams",
                schema: "match");
        }
    }
}
