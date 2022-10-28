namespace Habit_Tracker___Doveloop.Models.CosmosModels
{
    using Newtonsoft.Json;
    public class CosmosHabit
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }//unique key in database

        [JsonProperty(PropertyName = "habitId")]
        public int HabitID { get; set; }//key for habits

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "user")]
        public string User { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "labels")]
        public int[] Labels { get; set; }//when getting the user habits, just get everything in the HabitsLabels and then I get the labels with the labelIds in this array.
    }
}
