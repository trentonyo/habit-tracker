using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Habit_Tracker___Doveloop.Migrations
{
    public partial class AddedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "Label",
                type: "nvarchar(max)",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "HabitEntry",
                type: "nvarchar(max)",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "Habit",
                type: "nvarchar(max)",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User",
                table: "Label");

            migrationBuilder.DropColumn(
                name: "User",
                table: "HabitEntry");

            migrationBuilder.DropColumn(
                name: "User",
                table: "Habit");
        }
    }
}
