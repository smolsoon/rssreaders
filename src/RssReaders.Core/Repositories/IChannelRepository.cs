using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using RssReaders.Core.Model;

namespace RssReaders.Core.Repositories
{
    public interface IChannelRepository
    {
        Task<Channel> GetChannelAsync (ObjectId id);
        Task<IEnumerable<Channel>> GetAllChannelsAsync();
        Task AddChannelAsync (Channel channel);
        //Task UpdateChannelAsync(Channel channel);
        //Task DeleteChannelAsync(Channel channel);
    }
}
