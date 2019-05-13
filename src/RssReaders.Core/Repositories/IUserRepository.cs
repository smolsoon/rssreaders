using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using RssReaders.Core.Model;

namespace RssReaders.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(ObjectId id); 
        Task<User> GetAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);

    }
}