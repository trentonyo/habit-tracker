using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Habit_Tracker___Doveloop.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Day",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(9)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Day", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Label",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Label", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Habit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LabelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Habit_Label_LabelId",
                        column: x => x.LabelId,
                        principalTable: "Label",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HabitEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HabitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HabitEntry_Habit_HabitId",
                        column: x => x.HabitId,
                        principalTable: "Habit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Habit_LabelId",
                table: "Habit",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_HabitEntry_HabitId",
                table: "HabitEntry",
                column: "HabitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Day");

            migrationBuilder.DropTable(
                name: "HabitEntry");

            migrationBuilder.DropTable(
                name: "Habit");

            migrationBuilder.DropTable(
                name: "Label");
        }
    }
}
