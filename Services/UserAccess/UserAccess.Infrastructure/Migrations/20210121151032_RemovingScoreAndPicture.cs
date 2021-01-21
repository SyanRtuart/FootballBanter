using Microsoft.EntityFrameworkCore.Migrations;

namespace UserAccess.Infrastructure.Migrations
{
    public partial class RemovingScoreAndPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BanterScore",
                schema: "Users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CommentScore",
                schema: "Users",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BanterScore",
                schema: "Users",
                table: "Users",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CommentScore",
                schema: "Users",
                table: "Users",
                type: "int",
                nullable: true,
                defaultValue: 0);
        }
    }
}
