using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RssReaders.Core.Domain;
using RssReaders.Core.Repositories;
using RssReaders.Infrastructure.Database;
using RssReaders.Infrastructure.Settings;

namespace RssReaders.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthContext _database = null;
        public UserRepository(IOptions<DatabaseSettings> settings)
        {
            _database = new AuthContext(settings);
        }

        public async Task<User> GetAsync(Guid id)
            => await _database.Users.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<User> GetAsync(string email)
            => await _database.Users.AsQueryable().FirstOrDefaultAsync(x => x.Email == email);
        
        public async Task AddAsync(User user)
            => await _database.Users.InsertOneAsync(user);

        public async Task UpdateAsync(User user)
            => await _database.Users.ReplaceOneAsync(x => x.Id == user.Id, user);

        public async Task DeleteAsync(User user)
            => await _database.Users.InsertOneAsync(user);
        
    }
}