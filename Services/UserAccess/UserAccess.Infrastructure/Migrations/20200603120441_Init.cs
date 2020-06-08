using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserAccess.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                "user");

            migrationBuilder.CreateTable(
                "UserRegistrations",
                schema: "user",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    ConfirmedDate = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Login = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    RegisterDate = table.Column<DateTime>(),
                    StatusCode = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_UserRegistrations", x => x.Id); });

            migrationBuilder.CreateTable(
                "Users",
                schema: "user",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(),
                    LastName = table.Column<string>(nullable: true),
                    Login = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Users", x => x.Id); });

            migrationBuilder.CreateTable(
                "UserRoles",
                schema: "user",
                columns: table => new
                {
                    RoleCode = table.Column<string>(),
                    UserId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new {x.UserId, x.RoleCode});
                    table.ForeignKey(
                        "FK_UserRoles_Users_UserId",
                        x => x.UserId,
                        principalSchema: "user",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "UserRegistrations",
                "user");

            migrationBuilder.DropTable(
                "UserRoles",
                "user");

            migrationBuilder.DropTable(
                "Users",
                "user");
        }
    }
}