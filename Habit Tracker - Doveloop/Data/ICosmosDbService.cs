namespace Habit_Tracker___Doveloop.Data
{
    using Habit_Tracker___Doveloop.Models.CosmosModels;
    public interface ICosmosDbService
    {
        Task AddHabitAsync(CosmosHabit habit);
        Task DeleteHabitAsync(string id);
        Task<CosmosHabit> GetHabitAsync(string id);
        Task<IEnumerable<CosmosHabit>> GetHabitsAsync(string query);
        Task UpdateHabitAsync(CosmosHabit habit);
    }
}
