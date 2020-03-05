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
                name: "playerseq",
                schema: "match",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "teamseq",
                schema: "match",
                incrementBy: 10);

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

            migrationBuilder.CreateTable(
                name: "players",
                schema: "match",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_players_teams_TeamId",
                        column: x => x.TeamId,
                        principalSchema: "match",
                        principalTable: "teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_players_TeamId",
                schema: "match",
                table: "players",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "players",
                schema: "match");

            migrationBuilder.DropTable(
                name: "teams",
                schema: "match");

            migrationBuilder.DropSequence(
                name: "playerseq",
                schema: "match");

            migrationBuilder.DropSequence(
                name: "teamseq",
                schema: "match");
        }
    }
}
