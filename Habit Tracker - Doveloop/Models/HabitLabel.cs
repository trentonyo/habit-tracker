namespace Habit_Tracker___Doveloop.Models
{
    public class HabitLabel
    {
        public int HabitId { get; set; }
        public int LabelId { get; set; }
        public Habit Habit { get; set; }
        public Label Label { get; set; }
    }
}
