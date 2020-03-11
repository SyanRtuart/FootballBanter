﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Matches.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "match");

            migrationBuilder.CreateSequence(
                name: "matchseq",
                schema: "match",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "teamseq",
                schema: "match",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "matches",
                schema: "match",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    AwayTeamId = table.Column<int>(nullable: false),
                    HomeTeamId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
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
                    Id = table.Column<int>(nullable: false),
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

            migrationBuilder.DropSequence(
                name: "matchseq",
                schema: "match");

            migrationBuilder.DropSequence(
                name: "teamseq",
                schema: "match");
        }
    }
}