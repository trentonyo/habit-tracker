namespace Habit_Tracker___Doveloop.Models.ViewModels
{
    public class HabitViewModel
    {
        public Habit Habit { get; set; }
        public List<Label> Labels { get; set; }
        public List<Day> Days { get; set; }
        public List<HabitEntry> HabitEntries { get; set; }

        /*public HabitViewModel(Habit habit)
        {
            Habit = habit;
            Labels = new List<Label>();
            Days = new List<Day>();
            HabitEntries = habit.HabitEntries;

            habit.Labels.ForEach(hl => Labels.Add(hl.Label));
            habit.Days.ForEach(hd => Days.Add(hd.Day));
        }*/
    }
}
