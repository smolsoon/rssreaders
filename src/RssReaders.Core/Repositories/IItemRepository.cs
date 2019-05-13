using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using RssReaders.Core.Model;

namespace RssReaders.Core.Repositories
{
    public interface IItemRepository
    {
        Task<Item> GetItemAsync (ObjectId id);
        Task<IEnumerable<Item>> GetItemsAsync();
        Task AddItemsAsync(IEnumerable<Item> items);
        //Task AddItemAsync(Item item);
        //Task UpdateItemAsync(Item item);
        //Task DeleteItemAsync(Item item);
        
    }
}