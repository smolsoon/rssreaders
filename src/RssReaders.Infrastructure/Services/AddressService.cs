using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Bson;
using RssReaders.Core.Model;
using RssReaders.Core.Repositories;
using RssReaders.Infrastructure.DTO;

namespace RssReaders.Infrastructure.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public AddressService(IAddressRepository addressRepository, IItemRepository itemRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<AddressDTO> GetAddress(ObjectId id)
        {
            var address = await _addressRepository.GetAddressAsync(id);
            return _mapper.Map<AddressDTO>(address);
        }
        
        public string GetLink(ObjectId id)
            => _addressRepository.GetLink(id);

        public async Task CreateAddress(ObjectId id, string link, string category)
        {
            var address = await _addressRepository.GetAddressAsync(id);
            if(address == null)
            {
                throw new Exception($"Id's Address: '{id}' already exists.");
            }
            address = new Address(id, link, category);
            await _addressRepository.AddAddressAsync(address);
        }

        
    }
}