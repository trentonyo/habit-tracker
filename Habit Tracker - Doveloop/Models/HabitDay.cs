namespace Habit_Tracker___Doveloop.Models
{
    public class HabitDay
    {
        public int HabitId { get; set; }
        public int DayId { get; set; }
        public Habit Habit { get; set; }
        public Day Day { get; set; }
    }
}
