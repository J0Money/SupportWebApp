using Azure.Identity;
using Microsoft.Azure.Cosmos;
using SupportWebApp.Models;

namespace SupportWebApp.Services;


    public class CosmosSupportService
    {
        private readonly Container _container;

        public CosmosSupportService(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task AddMessageAsync(SupportMessage message)
        {
            await _container.CreateItemAsync(message, new PartitionKey(message.category));
        }

        public async Task<List<SupportMessage>> GetAllMessagesAsync()
        {
            var query = _container.GetItemQueryIterator<SupportMessage>("SELECT * FROM c");

            var results = new List<SupportMessage>();
            while (query.HasMoreResults)
            {
                foreach (var hest in await query.ReadNextAsync())
                {
                    results.Add(hest);
                }
            }

            return results.OrderByDescending(m => m.createdAt).ToList();
        }
    }
