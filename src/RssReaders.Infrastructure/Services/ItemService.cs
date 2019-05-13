using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Bson;
using RssReaders.Core.Model;
using RssReaders.Core.Repositories;
using RssReaders.Infrastructure.DTO;

namespace RssReaders.Infrastructure.Services
{
    public class ItemService : IItemService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        public ItemService(IAddressRepository addressRepository, IItemRepository itemRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemDTO>> GetItems()
        {
            var items = await _itemRepository.GetItemsAsync();
            return _mapper.Map<IEnumerable<ItemDTO>>(items);
        }

        public async Task CreateItems(ObjectId id, string title, string link, string description, DateTime pubDate)
        {   
            var item = await _itemRepository.GetItemAsync(id);
            if(item == null)
            {
                throw new Exception($"Items already exists.");
            }
            item = new Item(id, title, link, description, pubDate);
            await _itemRepository.AddItemAsync(item);
        }
    }
}