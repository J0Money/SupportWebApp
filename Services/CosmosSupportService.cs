using Azure.Identity;
using Microsoft.Azure.Cosmos;
using SupportWebApp.Models;

namespace SupportWebApp.Services;


    public class CosmosSupportService
    {
        private readonly Container _container;

        public CosmosSupportService(IConfiguration config)
        {
            var connectionString = config["Cosmos:ConnectionString"];
            var databaseName = "IBasSupportDB";
            var containerName = "ibassupport";

            var client = new CosmosClient(connectionString, new CosmosClientOptions
            {
                ApplicationName = "SupportWebApp"
            });

            _container = client.GetContainer(databaseName, containerName);
        }

        public async Task AddMessageAsync(SupportMessage message)
        {
            await _container.CreateItemAsync(message, new PartitionKey(message.category));
        }

        public async Task<List<SupportMessage>> GetAllMessagesAsync()
        {
            var query = _container.GetItemQueryIterator<SupportMessage>(
                new QueryDefinition("SELECT * FROM c"));

            var results = new List<SupportMessage>();
            while (query.HasMoreResults)
            {
                FeedResponse<SupportMessage> response = await query.ReadNextAsync();
                results.AddRange(response.Resource);
            }

            return results.OrderByDescending(m => m.createdAt).ToList();
        }
    }
