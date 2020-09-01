using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserAccess.Infrastructure.Migrations
{
    public partial class AddedScoresAndPictureToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BanterScore",
                schema: "Users",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommentScore",
                schema: "Users",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                schema: "Users",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BanterScore",
                schema: "Users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CommentScore",
                schema: "Users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Picture",
                schema: "Users",
                table: "Users");
        }
    }
}
