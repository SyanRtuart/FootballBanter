using Microsoft.EntityFrameworkCore.Migrations;

namespace Teams.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "team");

            migrationBuilder.CreateSequence(
                name: "playerseq",
                schema: "team",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "teamseq",
                schema: "team",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "teams",
                schema: "team",
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
                schema: "team",
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
                        principalSchema: "team",
                        principalTable: "teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_players_TeamId",
                schema: "team",
                table: "players",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "players",
                schema: "team");

            migrationBuilder.DropTable(
                name: "teams",
                schema: "team");

            migrationBuilder.DropSequence(
                name: "playerseq",
                schema: "team");

            migrationBuilder.DropSequence(
                name: "teamseq",
                schema: "team");
        }
    }
}
