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

            migrationBuilder.CreateTable(
                "phrases",
                schema: "phrase",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    DateCreated = table.Column<DateTime>(),
                    DateDeleted = table.Column<DateTime>(),
                    Description = table.Column<string>(),
                    IsDeleted = table.Column<bool>(),
                    MatchId = table.Column<Guid>(),
                    Positive = table.Column<bool>(),
                    Score = table.Column<int>(),
                    TeamId = table.Column<Guid>()
                },
                constraints: table => { table.PrimaryKey("PK_phrases", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "phrases",
                "phrase");
        }
    }
}