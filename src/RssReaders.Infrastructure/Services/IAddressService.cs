using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using RssReaders.Infrastructure.DTO;

namespace RssReaders.Infrastructure.Services
{
    public interface IAddressService
    {
        Task<AddressDTO> GetAddress(ObjectId id);
        string GetLink(ObjectId id);
        Task CreateAddress(ObjectId id, string link, string category);
    }
}