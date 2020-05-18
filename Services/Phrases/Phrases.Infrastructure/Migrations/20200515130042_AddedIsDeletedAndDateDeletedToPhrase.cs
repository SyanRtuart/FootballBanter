using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Phrases.Infrastructure.Migrations
{
    public partial class AddedIsDeletedAndDateDeletedToPhrase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateDeleted",
                schema: "phrase",
                table: "phrases",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "phrase",
                table: "phrases",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateDeleted",
                schema: "phrase",
                table: "phrases");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "phrase",
                table: "phrases");
        }
    }
}
