using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using RssReaders.Infrastructure.DTO;

namespace RssReaders.Infrastructure.Services
{
    public interface IItemService 
    {
        Task<IEnumerable<ItemDTO>> GetItems();
        Task CreateItems(ObjectId id, string title, string link, string description, DateTime pubDate);
        
    }
}