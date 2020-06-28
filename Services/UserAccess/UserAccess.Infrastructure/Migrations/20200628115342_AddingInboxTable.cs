using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserAccess.Infrastructure.Migrations
{
    public partial class AddingInboxTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InboxMessages",
                schema: "user",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OccurredOn = table.Column<DateTime>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    ProcessedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboxMessages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InboxMessages",
                schema: "user");
        }
    }
}
