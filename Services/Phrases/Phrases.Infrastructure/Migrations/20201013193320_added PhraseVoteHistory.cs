using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Phrases.Infrastructure.Migrations
{
    public partial class addedPhraseVoteHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhraseVoteHistory",
                schema: "Phrase",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PhraseId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    DateVoted = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhraseVoteHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhraseVoteHistory_Phrases_PhraseId",
                        column: x => x.PhraseId,
                        principalSchema: "Phrase",
                        principalTable: "Phrases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhraseVoteHistory_PhraseId",
                schema: "Phrase",
                table: "PhraseVoteHistory",
                column: "PhraseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhraseVoteHistory",
                schema: "Phrase");
        }
    }
}
