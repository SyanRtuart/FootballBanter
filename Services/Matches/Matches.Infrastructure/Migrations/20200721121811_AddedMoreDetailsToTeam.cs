using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Matches.Infrastructure.Migrations
{
    public partial class AddedMoreDetailsToTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StadiumDescription",
                schema: "Match",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StadiumLocation",
                schema: "Match",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StadiumName",
                schema: "Match",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                schema: "Match",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "Match",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                schema: "Match",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FormedYear",
                schema: "Match",
                table: "Teams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                schema: "Match",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "League",
                schema: "Match",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Logo",
                schema: "Match",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Manager",
                schema: "Match",
                table: "Teams",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StadiumDescription",
                schema: "Match",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "StadiumLocation",
                schema: "Match",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "StadiumName",
                schema: "Match",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Country",
                schema: "Match",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "Match",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Facebook",
                schema: "Match",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "FormedYear",
                schema: "Match",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Instagram",
                schema: "Match",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "League",
                schema: "Match",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Logo",
                schema: "Match",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Manager",
                schema: "Match",
                table: "Teams");
        }
    }
}
