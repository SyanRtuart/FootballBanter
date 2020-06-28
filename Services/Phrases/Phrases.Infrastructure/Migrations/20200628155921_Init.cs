using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Phrases.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Phrase");

            migrationBuilder.CreateTable(
                name: "InboxMessages",
                schema: "Phrase",
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
                schema: "Phrase",
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
                name: "OutboxMessages",
                schema: "Phrase",
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
                name: "Phrases",
                schema: "Phrase",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MatchId = table.Column<Guid>(nullable: false),
                    Positive = table.Column<bool>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    TeamId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phrases", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InboxMessages",
                schema: "Phrase");

            migrationBuilder.DropTable(
                name: "InternalCommands",
                schema: "Phrase");

            migrationBuilder.DropTable(
                name: "OutboxMessages",
                schema: "Phrase");

            migrationBuilder.DropTable(
                name: "Phrases",
                schema: "Phrase");
        }
    }
}
