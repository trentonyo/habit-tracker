//using System.ComponentModel.DataAnnotations;

namespace Habit_Tracker___Doveloop.Models
{
    public class HabitEntry
    {
        public int Id { get; set; }//database primary key
        public string? User { get; set; }
        //[DataType(DataType.Date)]//can use this to only use the day, not that day and time
        public DateTime EntryTime { get; set; }

        public int HabitId { get; set; }//database foreign key
        public Habit Habit { get; set; }
    }
}
