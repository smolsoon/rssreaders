using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RssReaders.Core.Model;
using RssReaders.Infrastructure.Settings;

namespace RssReaders.Infrastructure.Database
{
    public class AddressContext
    {
        private readonly IMongoDatabase _database = null;
        public AddressContext(IOptions<DatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Address> Address
        {
            get
            {
                return _database.GetCollection<Address>("Address");
            }
        }
    }
}