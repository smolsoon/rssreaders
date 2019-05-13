using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using RssReaders.Core.Model;

namespace RssReaders.Core.Repositories 
{
    public interface IAddressRepository 
    {
        Task<Address> GetAddressAsync (ObjectId id);
        Task<Address> GetLinkAddressAsync();
        string GetLink(ObjectId id);
        Task AddAddressAsync (Address address);
        Task UpdateAddressAsync(Address address);
        Task DeleteAddressAsync(Address address);
    }
}

//pobieranie linku z bazy danych
//pobieranie xml z linkow
//parsowanie xml listy itemow 
//post do bazy itemow