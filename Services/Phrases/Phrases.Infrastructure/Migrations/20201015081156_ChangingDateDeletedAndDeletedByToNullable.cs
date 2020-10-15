using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Phrases.Infrastructure.Migrations
{
    public partial class ChangingDateDeletedAndDeletedByToNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "DeletedByUserId",
                schema: "Phrase",
                table: "Phrases",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateDeleted",
                schema: "Phrase",
                table: "Phrases",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "DeletedByUserId",
                schema: "Phrase",
                table: "Phrases",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateDeleted",
                schema: "Phrase",
                table: "Phrases",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
