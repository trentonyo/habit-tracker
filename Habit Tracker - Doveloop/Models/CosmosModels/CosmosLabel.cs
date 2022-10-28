namespace Habit_Tracker___Doveloop.Models.CosmosModels
{
    using Newtonsoft.Json;
    public class CosmosLabel
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }//unique key in database

        [JsonProperty(PropertyName = "labelId")]
        public int LabelId { get; set; }//key for labels

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "user")]
        public string User { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
