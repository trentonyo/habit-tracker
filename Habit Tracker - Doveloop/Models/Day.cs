namespace Habit_Tracker___Doveloop.Models
{
    public class Day
    {
        ///can probably remove this class and just hardcode index "1-7" and days "Monday - Sunday" because that never changes
        /*public Day[] Days = new Day[] {
        new Day(1, "Monday"),
        new Day(2, "Tuesday"),
        new Day(3, "Wednesday"),
        new Day(4, "Thursday"),
        new Day(5, "Friday"),
        new Day(6, "Saturday"),
        new Day(7, "Sunday")
    };*/
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<HabitDay> Habits { get; set; }

        /*public Day(int id, string name)
        {
            Id = id;
            Name = name;
        }*/
    }
}
