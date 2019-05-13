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
    public class ChannelRepository : IChannelRepository
    {
        private readonly ChannelContext _database = null;
        private readonly IAddressRepository _addressRepository;

        public ChannelRepository(IOptions<DatabaseSettings> settings, IAddressRepository addressRepository)
        {
            _database = new ChannelContext(settings);
            _addressRepository = addressRepository;
        }
        public async Task<Channel> GetChannelAsync(ObjectId id)
            => await _database.Channels.AsQueryable().FirstOrDefaultAsync();

        public async Task<IEnumerable<Channel>> GetAllChannelsAsync()
            => await _database.Channels.Find(_ => true).ToListAsync();
        
        public async Task AddChannelAsync(Channel channel)
            => await _database.Channels.InsertOneAsync(channel);

        //public async Task UpdateChannelAsync(Channel channel)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task DeleteChannelAsync(Channel channel)
        //{
        //    throw new NotImplementedException();
        //}
    }
}