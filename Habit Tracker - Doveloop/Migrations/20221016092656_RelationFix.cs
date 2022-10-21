using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Habit_Tracker___Doveloop.Migrations
{
    public partial class RelationFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Habit_Label_LabelId",
                table: "Habit");

            migrationBuilder.DropIndex(
                name: "IX_Habit_LabelId",
                table: "Habit");

            migrationBuilder.DropColumn(
                name: "LabelId",
                table: "Habit");

            migrationBuilder.CreateTable(
                name: "DayHabit",
                columns: table => new
                {
                    DaysId = table.Column<int>(type: "int", nullable: false),
                    HabitsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayHabit", x => new { x.DaysId, x.HabitsId });
                    table.ForeignKey(
                        name: "FK_DayHabit_Day_DaysId",
                        column: x => x.DaysId,
                        principalTable: "Day",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DayHabit_Habit_HabitsId",
                        column: x => x.HabitsId,
                        principalTable: "Habit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HabitLabel",
                columns: table => new
                {
                    HabitsId = table.Column<int>(type: "int", nullable: false),
                    LabelsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitLabel", x => new { x.HabitsId, x.LabelsId });
                    table.ForeignKey(
                        name: "FK_HabitLabel_Habit_HabitsId",
                        column: x => x.HabitsId,
                        principalTable: "Habit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HabitLabel_Label_LabelsId",
                        column: x => x.LabelsId,
                        principalTable: "Label",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayHabit_HabitsId",
                table: "DayHabit",
                column: "HabitsId");

            migrationBuilder.CreateIndex(
                name: "IX_HabitLabel_LabelsId",
                table: "HabitLabel",
                column: "LabelsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayHabit");

            migrationBuilder.DropTable(
                name: "HabitLabel");

            migrationBuilder.AddColumn<int>(
                name: "LabelId",
                table: "Habit",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Habit_LabelId",
                table: "Habit",
                column: "LabelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Habit_Label_LabelId",
                table: "Habit",
                column: "LabelId",
                principalTable: "Label",
                principalColumn: "Id");
        }
    }
}
