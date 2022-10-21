namespace Habit_Tracker___Doveloop.Models
{
    public class Habit
    {
        public int Id { get; set; }//database primary key
        public string? User { get; set; }
        public string? Name { get; set; }
        public List<HabitLabel> Labels { get; set; }
        public List<HabitDay> Days { get; set; }
        public List<HabitEntry> HabitEntries { get; set; }
    }
}
