using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Habit_Tracker___Doveloop.Migrations
{
    public partial class AddedJoinTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayHabit");

            migrationBuilder.DropTable(
                name: "HabitLabel");

            migrationBuilder.CreateTable(
                name: "Habits_Days",
                columns: table => new
                {
                    HabitId = table.Column<int>(type: "int", nullable: false),
                    DayId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habits_Days", x => new { x.HabitId, x.DayId });
                    table.ForeignKey(
                        name: "FK_Habits_Days_Day_DayId",
                        column: x => x.DayId,
                        principalTable: "Day",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Habits_Days_Habit_HabitId",
                        column: x => x.HabitId,
                        principalTable: "Habit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Habits_Labels",
                columns: table => new
                {
                    HabitId = table.Column<int>(type: "int", nullable: false),
                    LabelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habits_Labels", x => new { x.HabitId, x.LabelId });
                    table.ForeignKey(
                        name: "FK_Habits_Labels_Habit_HabitId",
                        column: x => x.HabitId,
                        principalTable: "Habit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Habits_Labels_Label_LabelId",
                        column: x => x.LabelId,
                        principalTable: "Label",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Habits_Days_DayId",
                table: "Habits_Days",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_Habits_Labels_LabelId",
                table: "Habits_Labels",
                column: "LabelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Habits_Days");

            migrationBuilder.DropTable(
                name: "Habits_Labels");

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
    }
}
