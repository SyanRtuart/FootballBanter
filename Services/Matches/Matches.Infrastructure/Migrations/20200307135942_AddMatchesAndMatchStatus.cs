using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Matches.Infrastructure.Migrations
{
    public partial class AddMatchesAndMatchStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "matchseq",
                schema: "match",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "matchstatus",
                schema: "match",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matchstatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "matches",
                schema: "match",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    HomeTeamId = table.Column<int>(nullable: true),
                    AwayTeamId = table.Column<int>(nullable: true),
                    UtcDate1 = table.Column<DateTime>(nullable: false),
                    StatusId = table.Column<int>(nullable: true),
                    Score_Winner = table.Column<string>(nullable: true),
                    Score_HomeTeam = table.Column<int>(nullable: true),
                    Score_AwayTeam = table.Column<int>(nullable: true),
                    _awayTeamId = table.Column<int>(nullable: false),
                    _homeTeamId = table.Column<int>(nullable: false),
                    MatchStatusId = table.Column<int>(nullable: false),
                    UtcDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_matches_Team_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_matches_Team_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_matches_matchstatus_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "match",
                        principalTable: "matchstatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_matches_Team__awayTeamId",
                        column: x => x._awayTeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_matches_Team__homeTeamId",
                        column: x => x._homeTeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_matches_AwayTeamId",
                schema: "match",
                table: "matches",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_matches_HomeTeamId",
                schema: "match",
                table: "matches",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_matches_StatusId",
                schema: "match",
                table: "matches",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_matches__awayTeamId",
                schema: "match",
                table: "matches",
                column: "_awayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_matches__homeTeamId",
                schema: "match",
                table: "matches",
                column: "_homeTeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "matches",
                schema: "match");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "matchstatus",
                schema: "match");

            migrationBuilder.DropSequence(
                name: "matchseq",
                schema: "match");
        }
    }
}
