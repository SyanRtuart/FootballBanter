using Microsoft.EntityFrameworkCore.Migrations;

namespace Matches.Infrastructure.Migrations
{
    public partial class AddedNameExternalIdSeasonToMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                schema: "Match",
                table: "Matches",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "Match",
                table: "Matches",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Season",
                schema: "Match",
                table: "Matches",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId",
                schema: "Match",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "Match",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Season",
                schema: "Match",
                table: "Matches");
        }
    }
}
