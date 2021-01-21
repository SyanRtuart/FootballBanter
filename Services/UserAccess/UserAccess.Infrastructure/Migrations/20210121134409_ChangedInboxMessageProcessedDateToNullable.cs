using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserAccess.Infrastructure.Migrations
{
    public partial class ChangedInboxMessageProcessedDateToNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ProcessedDate",
                schema: "Users",
                table: "InboxMessages",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ProcessedDate",
                schema: "Users",
                table: "InboxMessages",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
