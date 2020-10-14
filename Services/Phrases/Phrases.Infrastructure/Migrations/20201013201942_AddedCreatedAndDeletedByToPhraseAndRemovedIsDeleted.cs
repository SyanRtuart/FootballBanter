using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Phrases.Infrastructure.Migrations
{
    public partial class AddedCreatedAndDeletedByToPhraseAndRemovedIsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Phrase",
                table: "Phrases");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserId",
                schema: "Phrase",
                table: "Phrases",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedByUserId",
                schema: "Phrase",
                table: "Phrases",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                schema: "Phrase",
                table: "Phrases");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                schema: "Phrase",
                table: "Phrases");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Phrase",
                table: "Phrases",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
