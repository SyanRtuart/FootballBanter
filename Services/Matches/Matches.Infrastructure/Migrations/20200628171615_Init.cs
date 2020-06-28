using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Matches.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Match");

            migrationBuilder.CreateTable(
                name: "InboxMessages",
                schema: "Match",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OccurredOn = table.Column<DateTime>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    ProcessedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InternalCommands",
                schema: "Match",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    ProcessedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalCommands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                schema: "Match",
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
                    table.PrimaryKey("PK_Matches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                schema: "Match",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OccurredOn = table.Column<DateTime>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    ProcessedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                schema: "Match",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InboxMessages",
                schema: "Match");

            migrationBuilder.DropTable(
                name: "InternalCommands",
                schema: "Match");

            migrationBuilder.DropTable(
                name: "Matches",
                schema: "Match");

            migrationBuilder.DropTable(
                name: "OutboxMessages",
                schema: "Match");

            migrationBuilder.DropTable(
                name: "Teams",
                schema: "Match");
        }
    }
}
