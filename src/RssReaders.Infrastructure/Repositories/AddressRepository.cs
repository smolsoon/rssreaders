using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using RssReaders.Core.Model;
using RssReaders.Core.Repositories;
using RssReaders.Infrastructure.Database;
using RssReaders.Infrastructure.Settings;

namespace RssReaders.Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AddressContext _database = null;
        public AddressRepository(IOptions<DatabaseSettings> settings)
        {
            _database = new AddressContext(settings);
        }

        public async Task<Address> GetAddressAsync(ObjectId id)
            => await _database.Address.AsQueryable().FirstOrDefaultAsync();

        public async Task<Address> GetLinkAddressAsync()
            => await _database.Address.AsQueryable().FirstAsync();

        public string GetLink(ObjectId id)
        {
            return _database.Address.AsQueryable().Where(x=>x.Id == id).ToString();
        }

        public async Task AddAddressAsync(Address address)
            => await _database.Address.InsertOneAsync(address);

        public Task UpdateAddressAsync(Address address)
            => throw new System.NotImplementedException();//await _database.Address.ReplaceOneAsync(x=>x._id == address._id);

        public async Task DeleteAddressAsync(Address address)
            => await _database.Address.DeleteOneAsync(x=>x.Id == address.Id);

    }
}