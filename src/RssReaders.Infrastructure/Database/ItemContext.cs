using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RssReaders.Core.Model;
using RssReaders.Infrastructure.Settings;

namespace RssReaders.Infrastructure.Database
{
    public class ItemContext
    {
        private readonly IMongoDatabase _database = null;
        public ItemContext(IOptions<DatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Item> Items
        {
            get
            {
                return _database.GetCollection<Item>("items");
            }
        }
    }
}