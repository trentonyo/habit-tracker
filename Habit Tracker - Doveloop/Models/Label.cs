namespace Habit_Tracker___Doveloop.Models
{
    public class Label
    {
        public int Id { get; set; }
        public string? User { get; set; }
        public string? Name { get; set; }

        public List<HabitLabel> Habits { get; set; }
    }
}
