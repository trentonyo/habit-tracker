namespace Habit_Tracker___Doveloop.Data
{
    using Microsoft.Azure.Cosmos;
    using Habit_Tracker___Doveloop.Models.CosmosModels;

    public class CosmosDbService : ICosmosDbService
    {
        private CosmosClient _client;
        private Container _habitContainer;
        private PartitionKey _partitionKey;

        public CosmosDbService(CosmosClient dbClient, string habitDbName, string containerName)
        {
            _client = dbClient;
            _habitContainer = _client.GetContainer(habitDbName, containerName);
        }

        public void SetUser(string user)
        {
            _partitionKey = new PartitionKey(user);
        }

        #region Habit
        public async Task AddHabitAsync(CosmosHabit habit)
        {
            await _habitContainer.CreateItemAsync<CosmosHabit>(habit, _partitionKey);
        }

        public async Task DeleteHabitAsync(string id)
        {
            await _habitContainer.DeleteItemAsync<CosmosHabit>(id, _partitionKey);
        }

        public async Task<CosmosHabit> GetHabitAsync(string id)
        {
            try
            {
                ItemResponse<CosmosHabit> response = await _habitContainer.ReadItemAsync<CosmosHabit>(id, _partitionKey);
                return response.Resource;
            } catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<CosmosHabit>> GetHabitsAsync(string queryString)
        {
            var query = _habitContainer.GetItemQueryIterator<CosmosHabit>(new QueryDefinition(queryString));
            List<CosmosHabit> results = new List<CosmosHabit>();
            while(query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task UpdateHabitAsync(CosmosHabit habit)
        {
            await _habitContainer.UpsertItemAsync(habit, _partitionKey);
        }
        #endregion

        #region Labels

        #endregion
    }
}
