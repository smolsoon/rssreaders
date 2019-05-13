using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using RssReaders.Core.Model;
using RssReaders.Core.Repositories;
using RssReaders.Infrastructure.Database;
using RssReaders.Infrastructure.Services;
using RssReaders.Infrastructure.Settings;

namespace RssReaders.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ItemContext _database = null;
        private readonly IAddressRepository _addressRepository;
        public ItemRepository(IOptions<DatabaseSettings> settings, IAddressRepository addressRepository)
        {
            _database = new ItemContext(settings);
            _addressRepository = addressRepository;
        }
        
        public async Task<Item> GetItemAsync(ObjectId id)
            => await _database.Items.AsQueryable().FirstOrDefaultAsync();

        public async Task<IEnumerable<Item>> GetItemsAsync()
            => await _database.Items.Find(_ => true).ToListAsync();

        public async Task AddItemsAsync(IEnumerable<Item> items)
            => await _database.Items.InsertManyAsync(items);

        //public async Task AddItemAsync(Item item)
        //{
        //    //var link = _addressRepository.GetLink(item._id);
        //    // var parsService = _parserService.ParseRss(link);
        //    // await _database.Items.InsertManyAsync(parsService);
        //}

        //public Task DeleteItemAsync(Item item)
        //{
        //    throw new System.NotImplementedException();
        //}

        
        //public Task UpdateItemAsync(Item item)
        //{
        //    throw new System.NotImplementedException();
        }

    
    }
}